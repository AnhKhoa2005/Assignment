using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] GameObject board;
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
            board.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            board.SetActive(false);
        }
    }
}
