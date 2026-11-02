using UnityEngine;

public class myplayer : MonoBehaviour
{
    private SpriteRenderer[] renderers; 
    private float invulnerablityTimer = 0f;
    private float flashTimer = 0f;
    private bool invulnerable = false;
    private bool flash = false;
    private int flashes = 1;

    public float speed = 3;
    public float jumpForce = 500f;
    public float health = 100f;
    public float invulnerablityTime = 0.18f;
    public float flashTime;

    Rigidbody2D body;
    Animator animator;
    Collider2D bodyCollider;

    public KeyCode punch = KeyCode.E;
    public KeyCode kick = KeyCode.Q;
    public KeyCode moveForwardKey = KeyCode.D;
    public KeyCode moveBackwardKey = KeyCode.A;
    public KeyCode jumpKey = KeyCode.W;

    public Collider2D fist;
    public Collider2D foot;
    public Material defaultMaterial;
    public Material flashMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<Collider2D>();

        renderers = GetComponentsInChildren<SpriteRenderer>();
        flashTime = invulnerablityTime / flashes;
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
        Jump();
        Movement();
        Attack("punch", punch);
        Attack("kick", kick);
        Flash();
        Invincibility();
    }

    private void ToggleFlash()
    {
        flash = !flash;

        foreach (SpriteRenderer renderer in renderers)
        {
            switch (flash)
            {
                case true:
                    renderer.material = flashMaterial;
                    break;
                case false:
                    renderer.material = defaultMaterial;
                    break;
            }
        }
    }

    private void Jump()
    {
        if (transform.position.y < -3.4f)
        {
            animator.SetBool("jumping",false);
            if (Input.GetKeyDown(jumpKey))
            {
                body.AddForce(Vector2.up * jumpForce);
            }
        } 
        else
        {
            animator.SetBool("jumping",true);
        }  
    }

    private void Movement()
    {
        int movementDirection = 0;//1 is forwards, -1 is backwards, 0 is still
        if(Input.GetKey(moveForwardKey))
        {
            movementDirection = 1;
  
        }        
        if(Input.GetKey(moveBackwardKey))
        {
            movementDirection = -1;
  
        }
        animator.SetInteger("movementDirection", movementDirection);
        body.linearVelocity = new Vector2(movementDirection * speed, body.linearVelocity.y);
    }

    private void Attack(string varible, KeyCode key) 
    {
        animator.SetBool(varible, false);

        if (Input.GetKey(key))
        {
            animator.SetBool(varible, true);

        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == fist || collision == foot)
        {
            Debug.Log("collided with self");
        }
        else
        {
            if (collision.gameObject.CompareTag("HurtBox") && !invulnerable)
            {
                health -= 5;
                // health = health -5
                animator.SetBool("hit02", true);
                invulnerable = true;
                ToggleFlash();
            }
        }
    }
    
    private void Animation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit02"))
        {
            animator.SetBool("hit02", false);
        }
    }

    private void Invincibility()
    {
        if (invulnerable)
        {
            invulnerablityTimer += Time.deltaTime;
            if (invulnerablityTimer >= invulnerablityTime)
            {
                invulnerable = false;
                invulnerablityTimer = 0f;

                if (flash)
                {
                    ToggleFlash();
                }
            }
        }
    }


    private void Flash()
    {
        if (invulnerable)
        {
            flashTimer += Time.deltaTime;
            if (flashTimer >= flashTime)
            {
                flashTimer = 0f;
                ToggleFlash();
            }
        }

    }
}