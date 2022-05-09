using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform toFollow;
    public float yValue;

    float initialZValue;

    // Start is called before the first frame update
    void Start()
    {
        initialZValue = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (toFollow != null)
        {
            transform.position = new Vector3(toFollow.position.x, yValue, initialZValue);
        }
    }
}
