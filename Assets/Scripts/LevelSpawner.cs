using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    LevelEditor LE;
    Ray myRay;
    RaycastHit hit;
    public LayerMask backdropLayer;
    public LayerMask groundLayer;
    

    private void Start()
    {
        LE = GameObject.FindObjectOfType<LevelEditor>();
    }

    public void SpawnPlatform() 
    {
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log(myRay.origin);
        
        if (Physics.Raycast(myRay, out hit, groundLayer))
        {
            int currLayer = hit.collider.gameObject.layer;

            if (currLayer == 6)
            {
                Debug.Log("Object Occupying Space");
            }
            else if (currLayer == 7)
            {
                Debug.Log("hit background");
                LE.SpawnPlatform();
            }
        }
        else
        {
            Debug.Log("Nothing");
        }
    }

    public void SpawnGoal()
    {
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log(myRay.origin);

        if (Physics.Raycast(myRay, out hit, groundLayer))
        {
            int currLayer = hit.collider.gameObject.layer;

            if (currLayer == 6)
            {
                Debug.Log("Object Occupying Space");
            }
            else if (currLayer == 7)
            {
                Debug.Log("hit background");
                LE.SpawnGoal();
            }
        }
        else
        {
            Debug.Log("Nothing");
        }
    }


    public void SpawnLava()
    {
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log(myRay.origin);

        if (Physics.Raycast(myRay, out hit, groundLayer))
        {
            int currLayer = hit.collider.gameObject.layer;

            if (currLayer == 6)
            {
                Debug.Log("Object Occupying Space");
            }
            else if (currLayer == 7)
            {
                Debug.Log("hit background");
                LE.SpawnLava();
            }
        }
        else
        {
            Debug.Log("Nothing");
        }
    }
    /*
    public void SpawnJumpPad()
    {
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log(myRay.origin);

        if (Physics.Raycast(myRay, out hit, groundLayer))
        {
            int currLayer = hit.collider.gameObject.layer;

            if (currLayer == 6)
            {
                Debug.Log("Object Occupying Space");
            }
            else if (currLayer == 7)
            {
                Debug.Log("hit background");
                LE.SpawnJumpPad();
            }
        }
        else
        {
            Debug.Log("Nothing");
        }
    }*/
}
