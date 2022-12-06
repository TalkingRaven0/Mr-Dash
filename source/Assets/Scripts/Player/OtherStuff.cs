using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OtherStuff : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement pm;
    private CapsuleCollider2D bc;
    private bool groundedchange;
    private float launchpos, launchvel;
    public float addgravity = 50f;
    private bool died = false;
    private bool InFinish;
    public GameObject gibs;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        bc = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (groundedchange != pm.grounded)
        {
            if (!pm.grounded)
            {
                launch();
            }
            else
                launchvel = 0;
            groundedchange = pm.grounded;
        }

        if (InFinish && Input.GetKeyDown(KeyCode.W))
            FindObjectOfType<GameManager>().FinishLevel();
    }

    private void FixedUpdate()
    {
        if (pm.grounded && pm.movedir == 0 || pm.movedir != Math.Sign(rb.velocity.x))
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, rb.velocity.y), 0.1f);

        if (transform.position.y-launchpos > (launchvel * 1/3) || rb.velocity.y < 0)
        {
            if (pm.wall)
            {
                rb.gravityScale = 0.5f;
                if (rb.velocity.y > 0)
                {
                    rb.gravityScale = 1;
                    rb.AddForce(Vector2.down * addgravity);
                }
            }
            else
            {
                rb.gravityScale = 1;
                rb.AddForce(Vector2.down * addgravity);
            }
        }

        if (pm.grounded && math.abs(rb.velocity.x) >= pm.movespeed)
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2( pm.movespeed * pm.movedir, rb.velocity.y), 0.001f);

        RaycastHit2D rc = Physics2D.Raycast(new Vector2(transform.position.x - bc.bounds.extents.x + 0.2f, transform.position.y - bc.bounds.extents.y-0.1f)
            , Vector2.right, bc.bounds.size.x - 0.4f, layerMask: (1<<10));
        if (rc.collider != null)
        {
            pm.grounded = true;
            rb.gravityScale = 1;
        }
        else
            pm.grounded = false;

        RaycastHit2D wc = Physics2D.BoxCast(transform.position, bc.bounds.size - new Vector3(-0.1f,1,0), 0,Vector2.zero,0.01f, layerMask: (1 << 10));
        if (wc.collider != null)
            pm.wall = true;
        else
            pm.wall = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
            InFinish = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
            InFinish = false;
    }

    public void Death(string cause)
    {
        if (died)
            return;
        died = true;
        GibsMovement.inheritforce(rb.velocity);
        FindObjectOfType<AudioManager>().Play("Death");
        gibs.transform.position = transform.position;
        gibs.SetActive(true);
        PlayerMovement.dashing = false;
        if(cause != null)
            FindObjectOfType<AudioManager>().Play(cause);
        FindObjectOfType<GameManager>().dead = true;
        Destroy(this.gameObject);
    }

    public void launch()
    {
        launchpos = transform.position.y;
        launchvel = rb.velocity.y;
    }
}
