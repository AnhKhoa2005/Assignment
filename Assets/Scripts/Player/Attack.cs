using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float dame;
    float countdown = 0.2f, timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= countdown)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void attack()
    {
        this.gameObject.SetActive(true);
        timer = 0;
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("enemy"))
        {
            enemy.GetComponent<Enemy>().GetHit(dame);
        }
    }
}
