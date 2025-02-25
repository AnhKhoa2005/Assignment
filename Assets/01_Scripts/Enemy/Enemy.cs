using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D enemy_rb;
    [SerializeField] Transform enemy_pos, A, B;
    [SerializeField] float speed = 5;
    [SerializeField] GameObject Attack;
    [SerializeField] Animator ani;
    [SerializeField] float Dame = 5;
    [SerializeField] float _HP = 50;
    [SerializeField] GameObject Damage_PopUp;
    [SerializeField] Image HP_Bar;

    private Vector3 dir;
    private bool IsDetectPlayerInMoveArea = false;
    public bool IsDetectPlayerInAttackArea { get; set; }
    private bool _isAttacking = false, died = false;
    float Current_HP, HP_Max;

    void Start()
    {
        dir = (B.position - enemy_pos.position).normalized;
        ani.CrossFade("run", 0);
        HP_Max = _HP;
    }

    void Update()
    {
        if (died)
        {
            dir = Vector3.zero;
            enemy_rb.linearVelocity = dir * speed;
            return;
        }

        if (IsDetectPlayerInAttackArea)
        {
            dir = Vector3.zero;
            enemy_rb.linearVelocity = dir * speed;

            if (!_isAttacking)
            {
                _isAttacking = true;
                StartCoroutine(EnemyAttack());
            }
            return;
        }

        Attack.SetActive(false);
        if (IsDetectPlayerInMoveArea && !_isAttacking)
        {
            ani.CrossFade("run", 0);
            return;
        }

        ani.CrossFade("run", 0);
        MoveDefault();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !died)
        {
            dir = (col.transform.position - enemy_pos.position).normalized;
            enemy_rb.linearVelocity = dir * speed;

            // Lật hướng enemy nếu cần
            enemy_pos.GetComponent<SpriteRenderer>().flipX = (enemy_pos.position.x > col.transform.position.x);

            IsDetectPlayerInMoveArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            IsDetectPlayerInMoveArea = false;
            IsDetectPlayerInAttackArea = false;
            StopCoroutine(EnemyAttack()); // Dừng attack ngay lập tức
            Attack.SetActive(false);
            _isAttacking = false;
        }
    }

    public void MoveDefault()
    {
        if (Vector2.Distance(enemy_pos.position, A.position) <= 2f)
        {
            enemy_pos.GetComponent<SpriteRenderer>().flipX = false;
            dir = (B.position - enemy_pos.position).normalized;
        }
        else if (Vector2.Distance(enemy_pos.position, B.position) <= 2f)
        {
            enemy_pos.GetComponent<SpriteRenderer>().flipX = true;
            dir = (A.position - enemy_pos.position).normalized;
        }
        enemy_rb.linearVelocity = new Vector2(dir.x * speed, enemy_rb.linearVelocity.y);
    }

    IEnumerator EnemyAttack()
    {
        while (IsDetectPlayerInAttackArea && !died)
        {
            Attack.SetActive(false);
            ani.CrossFade("idle", 0);
            yield return new WaitForSeconds(0.7f);
            ani.CrossFade("attack", 0);
            yield return new WaitForSeconds(0.1f);
            Attack.GetComponent<EnemyAttack>().init(Dame);
            Attack.SetActive(true);
            if (!enemy_pos.GetComponent<SpriteRenderer>().flipX)
            {
                Attack.transform.localPosition = new Vector3(1, 0, 0);
                Attack.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (enemy_pos.GetComponent<SpriteRenderer>().flipX)
            {
                Attack.transform.localPosition = new Vector3(-1, 0, 0);
                Attack.transform.localScale = new Vector3(-1, 1, 1);
            }
            yield return new WaitForSeconds(0.7f);
        }

        _isAttacking = false;
    }

    public void GetHit(float dame)
    {

        if (died) return;
        _HP -= dame;
        Current_HP = _HP;
        HP_Bar.fillAmount = Current_HP / HP_Max;
        float pos_x = Random.Range(-1, 1);
        float pos_y = Random.Range(-1, 1);
        Vector3 pos_enemy = enemy_pos.transform.position;
        pos_enemy.x += pos_x;
        pos_enemy.y += pos_y;
        GameObject d = Instantiate(Damage_PopUp, pos_enemy, Quaternion.identity);
        d.GetComponent<DamagePopUp>().init(dame);

        if (_HP <= 0)
        {

            died = true;
            StopAllCoroutines();
            Attack.SetActive(false);
            StartCoroutine(Die());
        }
        else
        {
            ani.CrossFade("gethit", 0);
            SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_Gethit);
        }
    }
    IEnumerator Die()
    {
        ani.CrossFade("die", 0);
        SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_Die);
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject, 3);
    }
}
