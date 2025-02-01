using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    [SerializeField] Enemy goblin;
    bool canAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        goblin.init(canAttack);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            canAttack = true;
        }
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            goblin.Target = goblin.B.position;
            goblin.Flip.flipX = false;
            canAttack = false;
        }
    }
}
