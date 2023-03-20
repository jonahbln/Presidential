using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public int startHealth = 100;
    public Slider healthSlider;
    public GameObject pickupDrop;

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
            //enemy death SFX
            Instantiate(pickupDrop, transform.position, transform.rotation);
            Destroy(gameObject, 0.15f);
        }

        if (collision.gameObject.CompareTag("PlayerBall"))
        {
            currentHealth -= 10;
            // enemy hit SFX
        }

        healthSlider.value = currentHealth;
    }
}
