using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem ps;
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * 30, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<OtherStuff>().Death("BulletKill");
        else
            FindObjectOfType<AudioManager>().Play("BulletHit");
        ps.Stop();
        ps.transform.parent = null;
        ps.transform.localScale = Vector3.one;
        Destroy(gameObject);
    }
}
