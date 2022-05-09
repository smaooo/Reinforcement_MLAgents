using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    Ray myRay;
    RaycastHit hit;
    public GameObject boundary;
    public GameObject food;
    public GameObject colony;
    float distance = 100;
    int backgroundLayer = 11;
    int waterLayer = 4;
    int foodLayer = 7;
    int nestLayer = 10;
    private MapController mapC;

    private void Start()
    {
        mapC = GameObject.FindObjectOfType<MapController>();
    }

    public void SpawnBoundary() 
    {
        int backgroundlayerMask = 1 << backgroundLayer;
        int waterLayerMask = 1 << waterLayer;
        int foodLayerMask = 1 << foodLayer;
        int nestLayerMask = 1 << nestLayer;
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //check to see if there is anything on the background, if not, spawn, if there is no spawn
        if (Physics.Raycast(myRay, out hit, waterLayerMask))
        {
            Debug.Log("hit water");
        }
        //if you hit the background cast a ray
        
        else if (Physics.Raycast(myRay, out hit, backgroundlayerMask)) 
        {
            Debug.Log("hit background");
            //spawn boundary/water
            mapC.SpawnBoundary();
        }
    }

    public void DespawnBoundary() 
    {
        int waterLayerMask = 1 << waterLayer;
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(myRay, out hit, waterLayerMask))
        {
            Debug.Log("hit water");//despawn the water
            Destroy(hit.transform.gameObject);

        }
    }

    public void SpawnFood() 
    {
        int backgroundlayerMask = 1 << backgroundLayer;
        int waterLayerMask = 1 << waterLayer;
        int foodLayerMask = 1 << foodLayer;
        int nestLayerMask = 1 << nestLayer;
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(myRay, out hit, waterLayerMask))//if there is water there, don't spawn food
        {
            Debug.Log("hit water");
        }
        if (Physics.Raycast(myRay, out hit, foodLayerMask))//if there is food there, also don't spawn food
        {
            Debug.Log("hit food");
        }
        
        else if (Physics.Raycast(myRay, out hit, backgroundlayerMask))//else if its on the background, spawn food
        {
            Debug.Log("hit background");
            //spawn food
            mapC.SpawnFood();
        }


    }

    public void DespawnFood() 
    {
        int foodLayerMask = 1 << foodLayer;
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(myRay, out hit, foodLayerMask))
        {
            Debug.Log("hit food");//despawn the food
            mapC.foodCount--; 
            Destroy(hit.transform.gameObject);

        }
    }

    public void SpawnColony() 
    {
        int backgroundlayerMask = 1 << backgroundLayer;
        int waterLayerMask = 1 << waterLayer;
        int foodLayerMask = 1 << foodLayer;
        int nestLayerMask = 1 << nestLayer;
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(myRay, out hit, waterLayerMask))//if there is water there, don't spawn food
        {
            Debug.Log("hit water");
        }
        else if (Physics.Raycast(myRay, out hit, backgroundlayerMask))//else if its on the background, spawn nest
        {
            Debug.Log("hit background");
            //spawn colony
            mapC.SpawnColony();
        }
    }

    public void SpawnColony2()
    {
        int backgroundlayerMask = 1 << backgroundLayer;
        int waterLayerMask = 1 << waterLayer;
        int foodLayerMask = 1 << foodLayer;
        int nestLayerMask = 1 << nestLayer;
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(myRay, out hit, waterLayerMask))//if there is water there, don't spawn food
        {
            Debug.Log("hit water");
        }
        else if (Physics.Raycast(myRay, out hit, backgroundlayerMask))//else if its on the background, spawn nest
        {
            Debug.Log("hit background");
            //spawn colony
            mapC.SpawnColony2();
        }
    }
    void Update()
    {
        
    }
}
