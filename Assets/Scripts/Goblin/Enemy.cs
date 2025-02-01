using Unity.Cinemachine;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform A, B;
    [SerializeField] GameObject player;
    [HideInInspector] public Vector3 Target;

    float speed = 5;
    float Distance_A, Distance_B;

    [HideInInspector] public SpriteRenderer Flip;
    Animator ani;
    Rigidbody2D rb;
    bool _canAttack;
    float _HP = 30;

    public void init(bool canAttack)
    {
        _canAttack = canAttack;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        Flip = GetComponent<SpriteRenderer>();
        Target = B.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
        if (_canAttack)
        {
            Target = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            if (this.transform.position.x < player.transform.position.x) Flip.flipX = false;
            else Flip.flipX = true;
        }
        else
        {
            Distance_A = Vector3.Distance(this.transform.position, A.position);
            Distance_B = Vector3.Distance(this.transform.position, B.position);
            if (Distance_A <= 0.1f)
            {
                Target = B.position;
                Flip.flipX = false;
            }
            else if (Distance_B <= 0.1f)
            {
                Target = A.position;
                Flip.flipX = true;
            }
        }
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 3)
            return;

        this.transform.position = Vector3.MoveTowards(this.transform.position, Target, speed * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        bool idle = (Vector3.Distance(this.transform.position, player.transform.position) <= 3) ? true : false;
        ani.SetBool("idle", idle);
    }

    public void GetHit(float dame)
    {
        _HP -= dame;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10);
        if (_HP <= 0) Destroy(gameObject);
    }
}
