using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string nextLevel = "";
    string currentLevel;
    public AudioClip winSFX;
    public AudioClip lossSFX;
    public Text gameText;
    public static float mouseSensitivity = 50f;
    public Canvas pauseMenu;

    public static bool gamePaused = false;
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        gamePaused = false;
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);

    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(lossSFX, Camera.main.transform.position);
        gameText.text = "YOU DIED!";
        gameText.gameObject.SetActive(true);
        Invoke("LoadCurrentLevel", 4f);
    }

    public void Win()
    {
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
        gameText.text = "YOU WIN!";
        gameText.gameObject.SetActive(true);
        Invoke("LoadNextLevel", 2f);
    }

    public void setSensitivity(float sens)
    {
        mouseSensitivity = sens;
    }
}
