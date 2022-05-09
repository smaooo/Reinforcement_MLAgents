using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float jumpPadForce;

    [Header("Ground Check")]
    public Transform groundCheckTransform;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    bool isGrounded;
    public GameObject startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPos.transform.position;
        Camera.main.GetComponent<FollowObject>().toFollow = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        Movement();
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Movement()
    {
        float speed = Input.GetAxis("Horizontal") * moveSpeed;
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
    }

    void Jump()
    {
        rb.velocity += (Vector2) (transform.up * jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            Win();
        }
        else if (collision.gameObject.CompareTag("JumpPad"))
        {
            rb.velocity += (Vector2)(transform.up * jumpPadForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            Win();
        }
    }
    void Die()
    {
        transform.position = startPos.transform.position;
    }

    void Win()
    {
        transform.position = startPos.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);

    }
}
