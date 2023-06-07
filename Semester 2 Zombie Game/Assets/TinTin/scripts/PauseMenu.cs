using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;
    private bool isPlaying;

    private void Start()
    {
        isPlaying = true;
        Resume();
    }
    void Update()
    {
        if (!(deathMenuUI.activeSelf)) {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPlaying)
                    Resume();
                else
                    Pause();
            }
        }
    }

    void Pause()
    {
        isPlaying = false;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f; // Freeze the game
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isPlaying = true;
        Time.timeScale = 1f; // Resume the game
    }

    public void Restart()
    {
        // Implement your restart logic here
        // For example, you can reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //public void Quit()
    //{
    //    // Implement your quit logic here
    //    // For example, you can exit the application
    //    Application.Quit();
    //}
}
