using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointBehavior : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private bool trainingMode = true; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn()
    {
        if (!trainingMode)
        {
            Instantiate(player);
            player.GetComponent<PlayerController>().startPos = this.gameObject;
            player.transform.position = this.transform.position;

        }
    }
}
