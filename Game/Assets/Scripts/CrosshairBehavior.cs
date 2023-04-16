using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairBehavior : MonoBehaviour
{
    public Image crosshairImage;
    public float speed = 5f;
    public Color color1 = Color.black;
    public Color color2 = Color.red;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.gamePaused)
        {
            crosshairImage.gameObject.SetActive(true);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    crosshairImage.GetComponent<RectTransform>().rotation = Quaternion.Lerp(crosshairImage.GetComponent<RectTransform>().rotation, Quaternion.Euler(0, 0, 180), Time.deltaTime * speed);
                    crosshairImage.GetComponent<Image>().color = Color.Lerp(crosshairImage.GetComponent<Image>().color, color2, Time.deltaTime * speed * 2f);
                    crosshairImage.GetComponent<RectTransform>().localScale = Vector3.Lerp(crosshairImage.GetComponent<RectTransform>().localScale, new Vector3(1.45f, 1.45f, 1.45f), Time.deltaTime * speed * 2f);
                }
                else
                {
                    crosshairImage.GetComponent<RectTransform>().rotation = Quaternion.Lerp(crosshairImage.GetComponent<RectTransform>().rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * speed * 1.5f);
                    crosshairImage.GetComponent<Image>().color = Color.Lerp(crosshairImage.GetComponent<Image>().color, color1, Time.deltaTime * speed * 7.5f);
                    crosshairImage.GetComponent<RectTransform>().localScale = Vector3.Lerp(crosshairImage.GetComponent<RectTransform>().localScale, new Vector3(1, 1, 1), Time.deltaTime * speed * 7.5f);
                }
            }
        }
        else
        {
            crosshairImage.gameObject.SetActive(false);
        }
    }
}
