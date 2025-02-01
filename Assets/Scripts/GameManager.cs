using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ShowMainMenu, ShowSetting, _ShowPauseMenu, ShowUI;
    // [Header("----------------Transition----------------")]
    [SerializeField] Animator transition;

    bool isPause = false, MainMenu = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ShowMainMenu != null || ShowSetting != null)
        {
            ShowMainMenu.SetActive(true);
            ShowSetting.SetActive(false);
        }
        if (_ShowPauseMenu != null)
        {
            ShowUI.SetActive(true);
            _ShowPauseMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            ShowPauseMenu();
            isPause = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            HidePauseMenu();
            isPause = false;
            Time.timeScale = 1;
        }
    }

    public void PlayGame()
    {
        if (transition != null) Debug.Log("duoc gan");
        transition.SetTrigger("end");
        SoundManager.instance.PlaySFX(SoundManager.instance.transition);
        Invoke("Wait", 1f);

    }

    public void Setting()
    {
        ShowMainMenu.SetActive(false);
        ShowSetting.SetActive(true);
    }

    public void Back()
    {
        ShowMainMenu.SetActive(true);
        ShowSetting.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackMainMenu()
    {
        if (transition == null) Debug.Log("chua gan");
        transition.SetTrigger("end");
        SoundManager.instance.PlaySFX(SoundManager.instance.transition);
        MainMenu = true;
        Time.timeScale = 1;
        Invoke("Wait", 1f);

    }

    public void ShowPauseMenu()
    {
        ShowUI.SetActive(false);
        _ShowPauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void HidePauseMenu()
    {
        ShowUI.SetActive(true);
        _ShowPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void Wait()
    {
        if (MainMenu)
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1;
        }
        MainMenu = false;
    }
}
