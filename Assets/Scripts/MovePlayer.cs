using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;
using NUnit.Framework.Interfaces;


public class movePlayer : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] float DashTime, speedDash, speed;
    [SerializeField] float jump;
    [SerializeField] Attack _attack;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform GroundCheck;
    [SerializeField] Slider energy_bar, HP_bar, Dash_bar;
    [SerializeField] Animator transition;
    float x, energy = 3, _HP = 100, dashTimer = 0, DashCoolDown = 2, RunTime = 0;
    Rigidbody2D rb;
    Animator ani;
    SpriteRenderer flip;
    bool isGrounded;
    bool DoubleJump, canDash = false, died = false;


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
        if (died) return;
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, ground);
        x = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);
        Flip();
        Jump();
        UpdateAnimation();

        //Tấn công
        if (energy <= 3)
            energy += Time.deltaTime;
        if (energy >= 1)
        {
            attack();
        }


        //Dash
        if (DashCoolDown < 2) DashCoolDown += Time.deltaTime;
        if (DashCoolDown >= 2)
        {
            Dash();
        }
        //Update UI
        energy_bar.value = energy;
        HP_bar.value = _HP;
        Dash_bar.value = DashCoolDown;

        //SFX
        Run_SFX();

    }

    void Run_SFX()
    {
        if (x != 0 && isGrounded)
        {
            RunTime += Time.deltaTime;
            if (RunTime >= 0.2)
            {
                SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_Run);
                RunTime = 0;
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            DoubleJump = true;
            SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_Jump);
        }
        else if (Input.GetKeyDown(KeyCode.W) && DoubleJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            DoubleJump = false;
            SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_Jump);
        }
    }
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
        ani.SetBool("run", running);
        ani.SetBool("jump", jumping);
    }

    void attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _attack.attack();
            ani.SetTrigger("attack");
            SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_Attack);
            energy--;
        }
    }

    public void GetHit(float dame)
    {
        if (died) return;
        _HP -= dame;

        if (_HP <= 0)
        {
            HP_bar.value = _HP;
            died = true;
            StartCoroutine(Die());
        }
        else
        {
            ani.SetTrigger("gethit");
            SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_Gethit);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10);
        }


    }
    IEnumerator Die()
    {
        ani.SetTrigger("die");
        SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_Die);
        yield return new WaitForSeconds(1);
        transition.SetTrigger("end");
        SoundManager.instance.PlaySFX(SoundManager.instance.transition);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void GetHP(float hp)
    {
        _HP += hp;
        if (_HP > 100) _HP = 100;
    }

    void Dash()
    {
        float dirDash = (flip.flipX == false) ? 1 : -1;
        if (Input.GetKeyDown(KeyCode.K))
        {
            canDash = true;
            SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_Dash);
        }
        ani.SetTrigger("dash");
        if (canDash && dashTimer < DashTime)
        {
            dashTimer += Time.deltaTime;
            rb.linearVelocity = new Vector2(speedDash * dirDash, rb.linearVelocity.y);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            GameObject _effect = Instantiate(effect, transform.position, Quaternion.identity);
            _effect.transform.rotation = (dirDash == 1) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            Destroy(_effect, 0.1f);
        }
        if (dashTimer >= DashTime)
        {
            canDash = false;
            dashTimer = 0;
            DashCoolDown = 0;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
