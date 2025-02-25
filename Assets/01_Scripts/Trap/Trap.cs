using System.Threading;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float Countdown = 0, _dame = 5;
    bool canGetHit = false;
    float time = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            canGetHit = true;
        }
    }

    void OnTriggerStay2D(Collider2D player)
    {
        if (canGetHit && player.CompareTag("Player"))
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                player.GetComponent<movePlayer>().GetHit(_dame);
                time = Countdown;
            }
        }
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            canGetHit = false;
        }
    }
}
