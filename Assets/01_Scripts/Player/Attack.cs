using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float dame = 7;
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
            if (enemy.TryGetComponent(out DetectPlayer detect)) // Lấy đúng DetectPlayer từ enemy
            {
                detect.init(dame);
            }
        }
    }
}
