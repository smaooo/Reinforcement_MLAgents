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
        if (!trainingMode)
        {
            Instantiate(player);
            player.GetComponent<PlayerController>().startPos = this.gameObject;

        }
    }
}
