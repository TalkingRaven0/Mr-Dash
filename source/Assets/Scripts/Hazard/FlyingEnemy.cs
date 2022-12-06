using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Vector3[] patrolpoint;
    public float speed = 5f;
    private int i = 0;
    public float fireinterval = 1;
    private float firecd;

    public void Update()
    {
        if (firecd > 0)
            firecd -= Time.deltaTime;
        else
        {
            RaycastHit2D info = Physics2D.Raycast(transform.position, Vector2.left, 50, layerMask: 1 << 8);
            if (info.collider != null)
                Fire();
        }
    }

    public void FixedUpdate()
    {
        if (patrolpoint.Length == 0)
            return;
        if (Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(patrolpoint[i].x) && Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(patrolpoint[i].y))
        {
            if (i >= patrolpoint.Length - 1)
                i = 0;
            else
                i += 1;
        }
        goTo(patrolpoint[i]);
    }

    public void Fire()
    {
        firecd = fireinterval;
        FindObjectOfType<AudioManager>().Play("Gunshot");
        Instantiate(bullet, transform.position + new Vector3(-0.07f, 0.021f, 0), transform.rotation);
    }

    public void goTo(Vector3 point)
    {
        transform.Translate((point-transform.position).normalized * Time.deltaTime * speed);
    }

}
