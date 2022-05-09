using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform toFollow;
    public float yValue;

    float initialZValue;
    float initialXValue;
    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
        initialZValue = transform.position.z;
        initialXValue = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (toFollow != null)
        {
            transform.position = new Vector3(toFollow.position.x, yValue, initialZValue);
        }
    }

    public void ResetPos()
    {
        toFollow = null;
        transform.position = originalPos;
    }
}
