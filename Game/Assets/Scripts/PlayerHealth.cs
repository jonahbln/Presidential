using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int startHealth = 100;
    public Slider healthSlider;

    int currentHealth;
    void Start()
    {
        currentHealth = startHealth;
        healthSlider.maxValue = startHealth;
        healthSlider.value = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (currentHealth <= 0)
        {
            // loss condition
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            FindObjectOfType<LevelManager>().Die();
        }

        if (collision.gameObject.CompareTag("EnemyBall"))
        {
            currentHealth -= 10;
            // player hit SFX
        }

        healthSlider.value = currentHealth;
    }

    public void AddHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, startHealth);
        healthSlider.value = currentHealth;
    }
}
