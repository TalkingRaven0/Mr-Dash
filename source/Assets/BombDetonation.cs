using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDetonation : MonoBehaviour
{
    public GameObject sprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.GetComponent<OtherStuff>() != null)
                collision.GetComponent<OtherStuff>().Death("Explosion");
            GetComponentInChildren<ExplosionForce>().Detonate();
            sprite.SetActive(false);
        }
    }
}
