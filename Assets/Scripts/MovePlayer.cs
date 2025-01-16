using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jump;
    float x;
    Rigidbody2D rb;
    Animator ani;
    SpriteRenderer flip;
    bool isGrounded;
    bool DoubleJump;
    // bool canDash = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        ani = gameObject.GetComponent<Animator>();
        flip = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);
        Flip();
        Jump();
        // Dash();
        UpdateAnimation();
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            DoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && DoubleJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            DoubleJump = false;
        }
    }
    // void Dash()
    // {
    //     if (Input.GetKeyDown(KeyCode.W) && isGrounded)
    //     {
    //         canDash = true;
    //         ani.SetBool("dash", false);

    //     }
    //     else if (Input.GetKeyDown(KeyCode.Space) && canDash && !isGrounded)
    //     {
    //         if (flip.flipX)
    //         {
    //             this.transform.position = new Vector2(this.transform.position.x - 2, this.transform.position.y);
    //         }
    //         else
    //         {
    //             this.transform.position = new Vector2(this.transform.position.x + 2, this.transform.position.y);

    //         }
    //         ani.SetBool("dash", true);
    //         canDash = false;
    //     }
    //     else ani.SetBool("dash", false);
    // }

    void Flip()
    {
        if (x > 0)
        {
            flip.flipX = false;
        }
        else if (x < 0)
        {
            flip.flipX = true;
        }
    }
    void UpdateAnimation()
    {
        bool running = x != 0;
        bool jumping = !isGrounded;
        bool attack = Input.GetKey(KeyCode.J);
        if (attack)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            running = false;
            jumping = false;
        }
        ani.SetBool("run", running);
        ani.SetBool("jump", jumping);
        ani.SetBool("attack", attack);

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ground"))
        {
            isGrounded = true;
        }
        Debug.Log(isGrounded);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
