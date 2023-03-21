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
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && nextLevel != "")
        {
            LoadNextLevel();
        }
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
        gameText.text = "MORE LEVELS COMING SOON!";
        gameText.gameObject.SetActive(true);
        Invoke("LoadCurrentLevel", 5f);
    }
}
