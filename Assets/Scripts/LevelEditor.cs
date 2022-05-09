using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{

    GameObject levelCam;
    public GameObject platform;
    public GameObject lava;
    public GameObject goal;
    //public GameObject jumpPad;
    public GameObject startPosPlayer;
    public GameObject startPosAI;
    public GameObject startPosDecoy;
    LevelSpawner levelSpawner;
    PAgent pAgent;
    [SerializeField]
    float speed = 1;
    float boundLeft;
    bool placingPlatform = false;
    bool placingLava = false;
    bool placingGoal = false;
    bool placingJumpPad = false;

    private void Start()
    {
        levelCam = Camera.main.gameObject;
        boundLeft = levelCam.transform.position.x;
        Debug.Log(levelCam.transform.position);
        levelSpawner = GameObject.FindObjectOfType<LevelSpawner>();
        
    }

    private void Update()
    {
        //pan camera to the left and right on left and arrow key press
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            PanLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            PanRight();
        }
        if (placingPlatform) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                levelSpawner.SpawnPlatform();
            }
        }
        if (placingLava) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                levelSpawner.SpawnLava();
            }
        }
        if (placingGoal) 
        {
            //limit to one goal
            if (Input.GetMouseButtonDown(0))
            {
                levelSpawner.SpawnGoal();
            }
        }
        /*
        if (placingJumpPad) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                levelSpawner.SpawnJumpPad();
            }
        }*/
    }

    void PanRight() 
    {
        if (levelCam.transform.position.x < 10)
        {
            levelCam.transform.Translate(new Vector3((speed * Time.deltaTime), 0, 0));

        }
    }

    void PanLeft() 
    {
        if (levelCam.transform.position.x > boundLeft) 
        {
            levelCam.transform.Translate(new Vector3((speed * Time.deltaTime)*-1, 0, 0));

        }
    }

    public void SpawnP(Vector3 positon)
    {
        Instantiate(platform).transform.position = RoundTransform(positon, 1f);
    }
    public void SpawnPlatform()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, platform.transform.position.z);
        SpawnP(adjustZ);
    }

    public void SpawnG(Vector3 positon)
    {
        Instantiate(goal).transform.position = RoundTransform(positon, 1f);
    }
    public void SpawnGoal()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, goal.transform.position.z);
        SpawnG(adjustZ);
    }

    public void SpawnL(Vector3 positon)
    {
        Instantiate(lava).transform.position = RoundTransform(positon, 1f);
    }
    public void SpawnLava()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, lava.transform.position.z);
        SpawnL(adjustZ);
    }

    /*
    public void SpawnJ(Vector3 positon)
    {
        Instantiate(jumpPad).transform.position = RoundTransform(positon, 1f);
    }
    public void SpawnJumpPad()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, jumpPad.transform.position.z);
        SpawnJ(adjustZ);
    }
    */
    private Vector3 RoundTransform(Vector3 v, float snapValue)
    {
        return new Vector3
        (
            snapValue * Mathf.Round(v.x / snapValue),
            snapValue * Mathf.Round(v.y / snapValue),
            v.z
        );
    }


    public void ButtonPlacePlatform() 
    {
        placingPlatform = true;
        placingGoal = false;
        placingLava = false;
    }

    public void ButtonPlaceLava() 
    {
        placingPlatform = false;
        placingLava = true;
        placingGoal = false;
    }

    public void ButtonPlaceGoal() 
    {
        placingPlatform = false;
        placingGoal = true;
        placingLava = false;
    }
    /*
    public void ButtonPlacingJumpPad() 
    {
        placingPlatform = false;
        placingGoal = false;
        placingLava = false;
        placingJumpPad = true;
    }
    */
    public void ButtonPlayerRun() 
    {
        //activate gameobject that holds start pos, as player is spawned on doing so
        startPosDecoy.SetActive(false);
        startPosPlayer.SetActive(true);
    }

    public void ButtonAIRun() 
    {
        startPosDecoy.SetActive(false);
        startPosAI.SetActive(true);
        
    }
}
