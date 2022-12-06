using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumppadstrength = 3f;
    public Transform point;
    private Vector2 direction;

    private void Awake()
    {
        direction = new Vector2 (point.position.x-transform.position.x,point.position.y-transform.position.y).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("JumpPad");
            collision.GetComponent<Rigidbody2D>().velocity = direction * collision.GetComponent<PlayerMovement>()
                .jumpstrength * jumppadstrength;
        }
    }
}
