using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject setting;
    [SerializeField] GameObject pauseMenu;
    bool isPaused = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        pauseMenu.SetActive(false);
        // Set the setting menu to inactive
        setting.SetActive(false);
        // Set the main menu to active
        mainMenu.SetActive(true);
    }

    public void Update()
    {
        ResumeGame();
    }
    public void StartGame()
    {
        // Load the next scene in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Setting()
    {
        // Set the setting menu to active
        setting.SetActive(true);
        // Set the main menu to inactive
        mainMenu.SetActive(false);
    }
    public void Back()
    {
        // Set the setting menu to inactive
        setting.SetActive(false);
        // Set the main menu to active
        mainMenu.SetActive(true);
    }
    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public void MainMenuScene()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
        // Set the setting menu to inactive
        setting.SetActive(false);
        // Set the main menu to active
        mainMenu.SetActive(true);
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                isPaused = false;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                isPaused = true;
            }
        }
    }

}
