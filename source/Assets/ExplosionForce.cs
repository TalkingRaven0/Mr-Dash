using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    private ParticleSystem explosion;

    private void Awake()
    {
        explosion = GetComponentInChildren<ParticleSystem>();
    }
    public void Detonate()
    {
        StartCoroutine(Explode());
        explosion.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 direction = collision.transform.position - transform.position;
        collision.GetComponent<Rigidbody2D>().AddForceAtPosition(direction.normalized*20,transform.position,ForceMode2D.Impulse);
    }

    IEnumerator Explode()
    {
        GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider2D>().enabled = false;
    }
}
