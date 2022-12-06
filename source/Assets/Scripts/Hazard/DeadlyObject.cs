using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.gameObject.GetComponent<OtherStuff>().Death("Spike");
    }
}
