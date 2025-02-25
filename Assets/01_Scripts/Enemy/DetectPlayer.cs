using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init(float dame)
    {
        enemy.GetHit(dame);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            enemy.IsDetectPlayerInAttackArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            enemy.IsDetectPlayerInAttackArea = false;
        }
    }
}
