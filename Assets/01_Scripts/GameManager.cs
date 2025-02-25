using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject ShowMainMenu, ShowSetting, _ShowPauseMenu, ShowUI, _Diamond, ChatBox, YouWin, Tutorial;
    // [Header("----------------Transition----------------")]
    [SerializeField] Animator transition;
    [SerializeField] TextMeshProUGUI Diamond_Icon, Diamond_text;

    bool isPause = false, MainMenu = false;
    public int Item { get; set; }
    public int Total_Diamond { get; set; }

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
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
            YouWin.SetActive(false);
            ShowUI.SetActive(true);
            _ShowPauseMenu.SetActive(false);
            ChatBox.SetActive(false);

        }
        if (Tutorial != null) Tutorial.SetActive(false);
        if (ShowMainMenu != null || ShowSetting != null) return;
        Item = 0;
        Total_Diamond = _Diamond.transform.childCount;
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
        if (ShowMainMenu != null || ShowSetting != null) return;
        Quantity_Diamond();

        Diamond_text.text = "Diamond: " + Item + "/" + Total_Diamond;
    }

    public void PlayGame()
    {
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
        ChatBox.SetActive(false);
        Tutorial.SetActive(false);
        _ShowPauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void HidePauseMenu()
    {
        ShowUI.SetActive(true);
        ChatBox.SetActive(false);
        Tutorial.SetActive(false);
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

    public void Quantity_Diamond()
    {
        Diamond_Icon.text = Item.ToString();
    }
}
