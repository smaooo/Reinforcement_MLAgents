using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLavaCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava"))
        {
            this.transform.parent.GetComponent<PAgent>().Die();
        }
    }
}
