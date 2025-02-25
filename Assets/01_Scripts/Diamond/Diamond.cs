using UnityEngine;

public class Diamond : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.audioClipSFX_HP);
            GameManager.instance.Item++;
            Destroy(gameObject);
        }
    }
}
