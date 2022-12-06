using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    public Transform gib;
    private Rigidbody2D rb;
    private GameManager gm;
    private float speedadjust;
    private Vector3 target;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if (gm.dead)
        {
            target = gib.position + new Vector3(0,3,-10);
            return;
        }

        if (PlayerMovement.dashing)
            speedadjust = 0;
        else
            speedadjust = rb.velocity.x;

        target = new Vector3(player.transform.position.x + speedadjust * 0.2f, player.transform.position.y + 2, -10);

    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target ,0.1f);
    }
}
