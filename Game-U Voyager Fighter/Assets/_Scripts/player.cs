// Copyright 2018 Game-U Enterprises, LLC
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 3f;
    [HideInInspector] public bool blocking; // public for the damage script
    public bool player2;
    public int health = 100;

    public GameObject fireball;
    float fireballCoolDownTimer;

    public AudioClip fireballSound;
    public AudioClip punchSound;
    public AudioClip kickSound;
    public AudioClip dieSound;

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        int movementDirection = 0; // 1 is forwards, -1 is backwards, 0 is still
        if (player2 == false)
        {
            // Player 1 movement
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movementDirection = 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movementDirection = -1;
            }

            GetComponent<Animator>().SetInteger("movementDirection", movementDirection);
        }
        else
        {
            // Player 2 movement
            if (Input.GetKey(KeyCode.D))
            {
                movementDirection = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementDirection = -1;
            }

            // Player 2 is flipped horizontally, so movementDirection is negated for the animator
            GetComponent<Animator>().SetInteger("movementDirection", -movementDirection);
        }

        //apply movement
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(movementDirection * speed, GetComponent<Rigidbody2D>().linearVelocity.y);

        //jumping          
        if (transform.position.y < -4.75f)
        {
            GetComponent<Animator>().SetBool("jumping", false);
            if (Input.GetKeyDown(KeyCode.UpArrow) && player2 == false)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 550f);
            }
            if (Input.GetKeyDown(KeyCode.W) && player2 == true)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 550f);
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("jumping", true);
        }

        //health
        if (health <= 0)
        {
            health = 0; // Ensure we never have negative health
            GetComponent<Animator>().SetBool("dead", true);
            GetComponent<AudioSource>().PlayOneShot(dieSound);
            enabled = false;
        }

        //increment the fireball "cool down" timer
        fireballCoolDownTimer = fireballCoolDownTimer + 1f;

        //combat
        if (player2 == false)
        {
            blocking = Input.GetKey(KeyCode.DownArrow);
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                GetComponent<Animator>().SetTrigger("punch");
                GetComponent<AudioSource>().PlayOneShot(punchSound);
            }
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                GetComponent<Animator>().SetTrigger("kick");
                GetComponent<AudioSource>().PlayOneShot(kickSound);
            }
            if (Input.GetKeyDown(KeyCode.Slash) && fireballCoolDownTimer > 30f)
            {
                Instantiate(fireball, transform.position + transform.right * 1.25f + transform.up * 2.25f, fireball.transform.rotation);
                GetComponent<Animator>().SetTrigger("fireball");
                GetComponent<AudioSource>().PlayOneShot(fireballSound);
                fireballCoolDownTimer = 0f; // reset the timer
            }
        }
        else
        {
            blocking = Input.GetKey(KeyCode.S);
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Animator>().SetTrigger("punch");
                GetComponent<AudioSource>().PlayOneShot(punchSound);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GetComponent<Animator>().SetTrigger("kick");
                GetComponent<AudioSource>().PlayOneShot(kickSound);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && fireballCoolDownTimer > 30f)
            {
                Instantiate(fireball, transform.position - transform.right * 1.25f + transform.up * 2.25f, fireball.transform.rotation);
                GetComponent<Animator>().SetTrigger("fireball");
                GetComponent<AudioSource>().PlayOneShot(fireballSound);
                fireballCoolDownTimer = 0f; // reset the timer
            }
        }

        GetComponent<Animator>().SetBool("blocking", blocking);
        if (blocking)
        {
            // halt horizontal velocity when blocking, but maintain vertical velocity
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, GetComponent<Rigidbody2D>().linearVelocity.y);
        }
    }
}
