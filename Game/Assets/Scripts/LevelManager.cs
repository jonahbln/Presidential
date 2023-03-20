using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextLevel = "";
    string currentLevel;
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
        // loss SFX
        Invoke("LoadCurrentLevel", 3f);
    }
}
