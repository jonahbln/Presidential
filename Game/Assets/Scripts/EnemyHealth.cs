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
    public GameObject deathPrefab;
    public AudioClip deathClip;

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
        if (currentHealth <= 0)
        {
            AlienSpawner.numberKilled++;
            Instantiate(pickupDrop, transform.position, transform.rotation);
            GameObject g = Instantiate(deathPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(g, 1f);
            AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("PlayerBall"))
        {
            currentHealth -= 10;
          
        }

        healthSlider.value = currentHealth;
    }
}
