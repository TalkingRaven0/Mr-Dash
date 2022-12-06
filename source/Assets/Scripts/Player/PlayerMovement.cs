using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool grounded, wall;
    public int movedir;
    private Rigidbody2D rb;
    private Animator a;
    private ParticleSystem dashE;
    public ParticleSystem dashhitE;
    private AudioManager sound;
    private OtherStuff os;
    private Vector3 initial;
    private Vector2 initialv;
    private int Grounded, Moving, Walled, Dashing;
    private int lookdir,dashdir;
    private bool _jump;
    public static bool dashing,dash_hit;
    private float dashcd=0.5f;
    public float dashlength = 10f;
    public float movespeed = 10f;
    public float jumpstrength = 13.5f;

    private void Awake()
    {
        os = GetComponent<OtherStuff>();
        rb = GetComponent<Rigidbody2D>();
        a = GetComponent<Animator>();
        sound = FindObjectOfType<AudioManager>();
        dashE = GetComponentInChildren<ParticleSystem>();
        Grounded = Animator.StringToHash("Grounded");
        Moving = Animator.StringToHash("Moving");
        Walled = Animator.StringToHash("Walled");
        Dashing = Animator.StringToHash("Dashing");
    }

    void Update()
    {
        lookdir = Math.Sign(transform.GetChild(0).position.x - transform.position.x);

        if (lookdir != movedir && movedir != 0)
            rotate();

        if (Input.GetKey(KeyCode.A))
        {
            movedir = -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            movedir = 1;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            movedir = 0;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _jump = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(dashcd<=0)
                if (movedir != 0)
                {
                    sound.Play("Dash");
                    initial = transform.position;
                    initialv = rb.velocity;
                    dashing = true;
                    dashdir = movedir;
                }
        }

        if (!dashing && dashcd >= 0)
            dashcd -= Time.deltaTime;

        a.SetBool(Grounded, grounded);
        a.SetBool(Dashing, dashing);
        if (rb.velocity.y < 0 && wall)
            a.SetBool(Walled, true);
        else
            a.SetBool(Walled, false);
    }

    private void FixedUpdate()
    {

        if (movedir != 0)
        {
            a.SetBool(Moving, true);
            move(movedir);
        }
        else
        {
            a.SetBool(Moving, false);
        }

        if (_jump)
            jump();

        if (dashing)
            dash(dashdir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("IsAnchor") && dashing)
        {
            initial = transform.position;
            dash_hit = true;
            sound.Play("Anchor");
            dashhitE.transform.position = transform.position;
            dashcd = 0f;
            os.launch();
            dashhitE.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IsAnchor") && dashing && !dash_hit)
        {
            initial = transform.position;
            dash_hit = true;
            sound.Play("Anchor");
            dashhitE.transform.position = transform.position;
            dashcd = 0f;
            os.launch();
            dashhitE.Play();
        }
    }

    private void move(int dir)
    {
        if (grounded)
        {
            if (math.abs(rb.velocity.x) <= movespeed)
                rb.velocity = new Vector2(movespeed * dir, rb.velocity.y);
            else
                rb.velocity = rb.velocity;
        }

        else
            rb.AddForce(Vector2.right * movespeed/2 * dir * Time.deltaTime * 30);
    }
    private void jump()
    {
        if (!grounded)
        {
            if (movedir != 0 && wall)
            {
                rb.gravityScale = 1;
                sound.Play("Jump");
                rb.velocity = new Vector2(movespeed * -movedir * 2, jumpstrength);
                os.launch();
            }

            _jump = false;
            return;
        }
        else
        {
            rb.AddForce(Vector2.up * jumpstrength, ForceMode2D.Impulse);
        }

        sound.Play("Jump");
        _jump = false;
    }

    private void dash(int dir)
    {
        if (math.sign(initialv.x) != math.sign(dir))
            initialv.x = 0;

        if(dash_hit)
        {
            rb.velocity = new Vector2(initialv.x + movespeed * 4.5f * dir, movespeed * 4.5f);
        }

        else
            rb.velocity = new Vector2(initialv.x + movespeed * 5 * dir, 0);


        dashE.Play();
        dashcd = 0.5f;

        if (Vector2.Distance(initial, transform.position) >= dashlength || wall)
        {
            if (dash_hit)
            {
                rb.AddForce(Vector2.down * movespeed * 3f, ForceMode2D.Impulse);
                rb.AddForce(Vector2.left * movespeed * 3f * dir, ForceMode2D.Impulse);
            }
            else
                rb.AddForce(Vector2.left * movespeed * 4.7f * dir, ForceMode2D.Impulse);

            dashing = false;
            dash_hit = false;
            dashE.Stop();
        }
    }

    private void rotate()
    {
        if (dashing || wall)
            return;
        transform.Rotate(0, 180, 0);
    }
}
