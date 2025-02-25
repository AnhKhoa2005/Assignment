using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.ChatBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            CheckNextLevel();
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (GameManager.instance.ChatBox != null)
                GameManager.instance.ChatBox.SetActive(false);
        }

    }

    void CheckNextLevel()
    {
        if (GameManager.instance._Diamond.transform.childCount == 0 && SceneManager.GetActiveScene().name == "Level_3")
        {
            GameManager.instance.YouWin.SetActive(true);
            SoundManager.instance.MusicBG.Stop();
            SoundManager.instance.PlaySFX(SoundManager.instance.YouWin);
        }
        else if (GameManager.instance._Diamond.transform.childCount == 0)
        {
            GameManager.instance.PlayGame();
        }
        else
        {
            GameManager.instance.ChatBox.SetActive(true);
        }
    }
}
