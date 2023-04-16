using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.GetComponent<Slider>().value = LevelManager.mouseSensitivity;
    }
    private void Update()
    {
        LevelManager.mouseSensitivity = slider.GetComponent<Slider>().value;
    }
}
