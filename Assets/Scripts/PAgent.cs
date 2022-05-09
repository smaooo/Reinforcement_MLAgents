using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System.Linq;
using Unity.MLAgents.Actuators;

public class PAgent : Agent
{

    new private Rigidbody2D rigidbody;
    [SerializeField]
    private bool trainingMode = false;
    [SerializeField]
    public float moveSpeed = 3;
    public float jumpForce = 8;
    private bool frozen = false;

    // SHOULD SET FROM LEVEL
    private Transform destPoint;
    private Transform startPoint;
    private bool isGrounded;
    [Header("Ground Check")]
    public Transform groundCheckTransform;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private Vector2 prevPos;
    private float episodeReward;
    [SerializeField]
    private List<GameObject> lavas;
    private int winCount;
    public bool continous = false;
    private void Start()
    {
        if (!trainingMode)
        {
            startPoint = GameObject.FindGameObjectWithTag("StartPosition").transform;
            destPoint = GameObject.FindGameObjectWithTag("Goal").transform;
            rigidbody = this.GetComponent<Rigidbody2D>();


        }

    }
    public override void Initialize()
    {
        if (trainingMode)
        {
            startPoint = GameObject.FindGameObjectsWithTag("StartPosition").ToList().Find(x => x.transform.parent == this.transform.parent).transform;
            destPoint = GameObject.FindGameObjectsWithTag("Goal").ToList().Find(x => x.transform.parent == this.transform.parent).transform;

        }
        else
        {
            MaxStep = 0;
        }
        rigidbody = this.GetComponent<Rigidbody2D>();

      

    }

    public override void OnEpisodeBegin()
    {
        episodeReward = 0;
        prevPos = this.transform.position;
        this.transform.position = startPoint.position;
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        isGrounded = false;

    }

    public override void OnActionReceived(ActionBuffers actions)

    {

        // 0: (+1) Move Forward / (-1) Move Backward
        // 1: (+1) Jump

        if (frozen)
            return;
        
        if (actions.ContinuousActions.Length > 0)
        {

            if (actions.ContinuousActions[0] != 0)
            {
                Movement(actions.ContinuousActions[0]);
            }
            else
            {
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
            if (isGrounded && actions.ContinuousActions[1] >= 0)
            {
                Jump(actions.ContinuousActions[1]);
            }
        }
        else
        {
            if (actions.DiscreteActions[0] != 0)
            {
                Movement(actions.DiscreteActions[0]);
            }
            else
            {
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
            if (isGrounded && actions.DiscreteActions[1] >= 0)
            {
                Jump(actions.DiscreteActions[1]);
            }
        }
        

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //base.CollectObservations(sensor);

        //sensor.AddObservation((Vector2)this.transform.position);
        sensor.AddObservation(Vector2.Distance(this.transform.position, destPoint.position));
        //foreach (var w in GameObject.FindGameObjectsWithTag("Walkable"))
        //{
        //    if (w.transform.parent == this.transform.parent)
        //        sensor.AddObservation((Vector2)w.transform.position);
        //}

        foreach (var l in GameObject.FindGameObjectsWithTag("Lava"))
        {
            if (trainingMode)
            {
                if (l.transform.parent == this.transform.parent)
                {
                    sensor.AddObservation((Vector2)l.transform.position);
                    sensor.AddObservation(Vector2.Dot(this.transform.right, (l.transform.position - this.transform.position).normalized));
                    sensor.AddObservation(Vector2.Distance(this.transform.position, l.transform.position));
                }

            }
            else
            {
                sensor.AddObservation((Vector2)l.transform.position);
                sensor.AddObservation(Vector2.Dot(this.transform.right, (l.transform.position - this.transform.position).normalized));
                sensor.AddObservation(Vector2.Distance(this.transform.position, l.transform.position));
            }
        }

        //foreach (var h in GameObject.FindGameObjectsWithTag("Hole"))
        //{
        //    if (h.transform.parent == this.transform.parent)
        //        sensor.AddObservation((Vector2)h.transform.position);
        //}
        sensor.AddObservation((Vector2)destPoint.transform.position);

        sensor.AddObservation(isGrounded);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if (continous)
        {
            ActionSegment<float> cont = actionsOut.ContinuousActions;
            cont[0] = Input.GetAxisRaw("Horizontal");
            cont[1] = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;

        }
        else
        {
            ActionSegment<int> cont = actionsOut.DiscreteActions;
            cont[0] = (int)Input.GetAxisRaw("Horizontal");
            cont[1] = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;
        }

        

    }

    public void FreezeAgent()
    {
        Debug.Assert(trainingMode == false, "Freeze/Unforze not supported in training");

        frozen = true;

        rigidbody.Sleep();
    }

    public void UnFreezeAgent()
    {
        Debug.Assert(trainingMode == false, "Freeze/Unforze not supported in training");

        rigidbody.WakeUp();
    }

    private void Update()
    {
        CheckGrounded();
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    Jump();
        //}
        //if (!isGrounded)
        // {
        //     AddReward(-0.05f);
        // }
        if (trainingMode)
        {
            if (winCount % 500 == 0 && winCount != 0)
            {
                for (int i = 0; i < lavas.Count; i++)
                {
                    if (lavas[i].tag != "Lava")
                    {
                        lavas[i].tag = "Lava";
                        break;
                    }
                }
                winCount = 0;
            }

        }
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            //AddReward(-0.5f);
            //float point = Mathf.Clamp(Mathf.InverseLerp(0, Vector2.Distance(startPoint.position, destPoint.position), Vector2.Distance(this.transform.position, destPoint.position)),
            //    0, Vector2.Distance(startPoint.position, destPoint.position)) / 10;
            //AddReward(point);
            Die();
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            AddReward(0.1f);
            winCount++;
            Win();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            AddReward(-0.5f);
            //AddReward(-episodeReward);
            //float point = Mathf.Clamp(Mathf.InverseLerp(0, Vector2.Distance(startPoint.position, destPoint.position), Vector2.Distance(this.transform.position, destPoint.position)),
            //    0, Vector2.Distance(startPoint.position, destPoint.position)) / 10;
            //AddReward(point);
            print("touched");
            Die();
        }
      
        //else if (collision.gameObject.CompareTag("JumpPad"))
        //{
        //    rb.velocity += (Vector2)(transform.up * jumpPadForce);
        //}


    }

    public void Die()
    {
      
        this.transform.position = startPoint.position;
        
    }
    void Win()
    {
        transform.position = startPoint.transform.position;
        //if (trainingMode)
        //{
        //    EndEpisode();
        //}
        //else
        //{

        //}
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
    }

    void Jump(float amount)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x,jumpForce * amount);
    }

    void Movement(float direction)
    {
        
        float speed = moveSpeed;
        rigidbody.velocity = new Vector2(speed * direction, rigidbody.velocity.y);

        float dist = Vector2.Distance(this.transform.position, destPoint.position);
        float prevDist = Vector2.Distance(prevPos, destPoint.position);
        if (dist > prevDist)
        {
            //episodeReward += 0.005f;
            //AddReward(0.005f);
            AddReward(-0.01f);
        }
        //else
        //{
        //}
        prevPos = this.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);

    }
}


