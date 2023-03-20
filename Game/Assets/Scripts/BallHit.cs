using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallHit : MonoBehaviour
{

    public int health = 100;
    public Slider healthSlider;

    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (health <= 0)
        {
            if(gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            health -= 10;
        }

        healthSlider.value = health;
    }
}
