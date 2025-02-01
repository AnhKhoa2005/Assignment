using UnityEngine;

public class HP : MonoBehaviour
{
    float _HP = 20;
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
            player.GetComponent<movePlayer>().GetHP(_HP);
            Destroy(gameObject);
        }
    }
}
