using Unity.Cinemachine;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float Dame;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init(float dame)
    {
        this.Dame = dame;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.TryGetComponent(out movePlayer player))
            {
                player.GetHit(Dame);
            }

        }
    }
}
