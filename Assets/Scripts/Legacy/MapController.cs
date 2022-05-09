using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MapController : MonoBehaviour
{
    //[SerializeField] private PathfindingVisual pathfindingVisual;
    //private Pathfinding pathfinding;
    public TMP_Text placingText;
    public TMP_Text toggleText;
    public TMP_Text antCountText;
    public TMP_Text foodTotalText;
    public TMP_Text colony1TotalText;
    public TMP_Text colony2TotalText;
    private bool placingFood = false;
    private bool placingBoundary = false;
    private bool placingColony = false;
    private bool placingColony2 = false;
    private bool erasingBoundary = false;
    private bool erasingFood = false;
    private bool erasingColony = false;
    public bool multiColony = false;
    private int colonyCount = 0;
    private int colony2Count = 0;
    Vector3 worldPosition;
    public GameObject boundary;
    public GameObject food;
    public GameObject colony;
    public GameObject ant;
    public GameObject colony2;
    public GameObject ant2;
    public GameObject antError;
    public GameObject foodError;
    public GameObject colonyError;
    private Spawner spawner;
    private int antCount = 0;
    private int antCountMax = 20;
    [HideInInspector]
    public int foodCount = 0;
    private bool hasColony = false;
    private bool hasFood = false;
    private bool hasAnts = false;
    private bool gameOn = false;
    private int foodTotal = 0;
    public int totalFoodVal = 0;
    private int totalFoodValForChecking = 0;
    public int totalFoodValColony1 = 0;
    public int totalFoodValColony2 = 0;


    private void Start()
    {
        //pathfinding = new Pathfinding(20, 10);
        //pathfindingVisual.SetGrid(pathfinding.GetGrid());
        spawner = GameObject.FindObjectOfType<Spawner>();
        
    }

    private void Update()
    {
        if (!gameOn) 
        {
            CalculateTotal("Food");
        }
        if (gameOn) 
        {
            if (totalFoodVal == 0 && totalFoodValColony1 + totalFoodValColony2 == totalFoodValForChecking) 
            {
                Time.timeScale = 0;
            }
            foodTotalText.text = totalFoodVal.ToString();
            colony1TotalText.text = totalFoodValColony1.ToString();
            colony2TotalText.text = totalFoodValColony2.ToString();
        }
        if (antCount > 0)
        {
            hasAnts = true;
        }
        else { hasAnts = false; }

        if (foodCount > 0)
        {
            hasFood = true;
        }
        else { hasFood = false; }

        if (colonyCount > 0) 
        {
            hasColony = true;
        }
        else { hasColony = false; }

        if (placingBoundary) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                //SpawnBoundary();
                spawner.SpawnBoundary();
            }
        }

        

        if (erasingBoundary) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                //EraseBoundary();
                spawner.DespawnBoundary();
                
            }
        }
        if (placingFood)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //SpawnFood();
                spawner.SpawnFood();
                foodCount++;

            }
        }
        if (erasingFood) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                spawner.DespawnFood();

            }
        }
        if (placingColony)
        {
            if (colonyCount < 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //ResetColony();
                    spawner.SpawnColony();
                    colonyCount++;
                    //SpawnColony();

                }
            }
            
        }

        if (placingColony2) 
        {
            if (colony2Count < 1) 
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //ResetColony();
                    spawner.SpawnColony2();
                    colony2Count++;
                    //SpawnColony();

                }
            }
        }

    }
    
   

    
    private void DestroyAll(string tag) 
    {
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < toDestroy.Length; i++) 
        {
            GameObject.Destroy(toDestroy[i]);
        }
    }

    private void CalculateTotal(string tag) 
    {
        GameObject[] totalOf = GameObject.FindGameObjectsWithTag(tag);
        foodTotalText.text = (totalOf.Length / 2).ToString();
    }

    private int CalculateTotalFood(string tag) 
    {
        GameObject[] totalOf = GameObject.FindGameObjectsWithTag(tag);
        return (totalOf.Length / 2);
    }

    public void ResetBoundary() 
    {
        DestroyAll("Boundary");
    }

    public void ResetFood()
    {
        DestroyAll("Food");
    }
    public void ResetColony()
    {
        colonyCount = 0;
        DestroyAll("Colony");
    }

    public void ResetColony2() 
    {
        colony2Count = 0;
        DestroyAll("Colony2");
    }

    public void ResetAnts() 
    {
        DestroyAll("Ant");
    }
   
    private void EraseBoundary()
    {
      
    }

    public void SpawnB(Vector3 positon) 
    {
        Instantiate(boundary).transform.position = positon;
    }
    public void SpawnBoundary()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, boundary.transform.position.z);
        SpawnB(adjustZ);
    }

    public void SpawnF(Vector3 positon)
    {
        Instantiate(food).transform.position = positon;
    }
    public void SpawnFood()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, food.transform.position.z);
        SpawnF(adjustZ);
    }

    public void SpawnC(Vector3 positon)
    {
        Instantiate(colony).transform.position = positon;
    }
    public void SpawnColony()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, colony.transform.position.z);
        SpawnC(adjustZ);
    }

    public void SpawnC2(Vector3 positon)
    {
        Instantiate(colony2).transform.position = positon;
    }
    public void SpawnColony2()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, colony2.transform.position.z);
        SpawnC2(adjustZ);
    }
    public void PlacingBoundary() 
    {
        placingBoundary = true;
        placingColony = false;
        placingColony2 = false;
        placingFood = false;
        erasingBoundary = false;
        erasingFood = false;
        erasingColony = false;
        placingText.text = "Water";
        toggleText.text = "Placing:";
       
    }

    public void PlacingColony()
    {
        placingBoundary = false;
        placingColony = true;
        placingColony2 = false;
        placingFood = false;
        erasingBoundary = false;
        erasingFood = false;
        erasingColony = false;
        placingText.text = "Colony 1";
        toggleText.text = "Placing:";
  
    }

    public void PlacingColony2()
    {
        placingBoundary = false;
        placingColony = false;
        placingColony2 = true;
        placingFood = false;
        erasingBoundary = false;
        erasingFood = false;
        erasingColony = false;
        placingText.text = "Colony 2";
        toggleText.text = "Placing:";

    }

    public void PlacingFood()
    {
        placingBoundary = false;
        placingColony = false;
        placingColony2 = false;
        placingFood = true;
        erasingBoundary = false;
        erasingFood = false;
        erasingColony = false;
        placingText.text = "Food";
        toggleText.text = "Placing:";
    
    }

    public void UndoBoundary() 
    {
        erasingBoundary = true;
        erasingFood = false;
        erasingColony = false;
        placingBoundary = false;
        placingColony = false;
        placingColony2 = false;
        placingFood = false;
        toggleText.text = "Erasing:";
        placingText.text = "Water";

    }

    public void UndoFood() 
    {
        erasingBoundary = false;
        erasingFood = true;
        erasingColony = false;
        placingBoundary = false;
        placingColony = false;
        placingColony2 = false;
        placingFood = false;
        toggleText.text = "Erasing:";
        placingText.text = "Food";
    }

    public void ButtonIncreaseSmall() 
    {
        if (antCount < antCountMax) 
        {
            antCount++;
            antCountText.text = antCount.ToString();
        }
    }

    public void ButtonIncreaseBig() 
    {
        if (antCount < antCountMax -4)
        {
            antCount += 5;
            antCountText.text = antCount.ToString();
        }

    }

    public void ButtonDecreaseSmall() 
    {
        if (antCount > 0) 
        {
            antCount--;
            antCountText.text = antCount.ToString();
        }
    }

    public void ButtonDecreaseBig() 
    {
        if (antCount > 4) 
        {
            antCount-= 5;
            antCountText.text = antCount.ToString();
        }
    }

    public void ButtonPauseUnpause() 
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    IEnumerator DisplayError(GameObject obj) 
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(3f);
        obj.SetActive(false);
    }

    IEnumerator DisplayFoodError() 
    {
        foodError.SetActive(true);
        yield return new WaitForSeconds(3f);
        foodError.SetActive(false);
    }
    public void ButtonPlay() 
    {
        if (!gameOn) 
        {
            //store total food value on play
            
            //check for colony
            //no colony, message
            if (!hasColony) 
            {
                //display no oclony message
                StartCoroutine(DisplayError(colonyError));
            }
            //check for ant# greater than 0
            //no ants, message
            if (!hasAnts) 
            {
                //display not ants message
                StartCoroutine(DisplayError(antError));
            }
            //check for food# greater than 0
            //no food, message
            if (!hasFood)
            {
                //display no food message
                StartCoroutine(DisplayError(foodError));
            }
            //take antcount, spawn antcount# in colony
            else 
            {
                totalFoodVal = CalculateTotalFood("Food");
                totalFoodValForChecking = CalculateTotalFood("Food");

                gameOn = true;
                //Debug.Log("we have ants: " + antCount);
                var col = GameObject.FindGameObjectWithTag("Colony");
                //Debug.Log("they will spawn at: " + col.transform.position);
                if (col != null) 
                { 
                    for (int i = 0; i < antCount; i++) 
                    {
                        var inst = Instantiate(ant, col.transform.position, Quaternion.identity);
                        //inst.GetComponent<AntBehavior>().isAnt1 = true;
                    }
                }

                
                var col2 = GameObject.FindGameObjectWithTag("Colony2");
                if (col2 != null) 
                { 
                    for (int i = 0; i < antCount; i++)
                    {
                        var inst = Instantiate(ant2, col2.transform.position, Quaternion.identity);
                        //inst.GetComponent<AntBehavior>().isAnt1 = false;
                    }
                }
            }


        }
    }

    public void ButtonResetAll() 
    {
        gameOn = false;
        ResetBoundary();
        ResetColony();
        ResetColony2();
        ResetFood();
        ResetAnts();
        totalFoodVal = 0;
        totalFoodValForChecking = 0;
        totalFoodValColony1 = 0;
        totalFoodValColony2 = 0;
        colony1TotalText.text = totalFoodValColony1.ToString();
        colony2TotalText.text = totalFoodValColony2.ToString();
        Time.timeScale = 1;
    }
    
}
