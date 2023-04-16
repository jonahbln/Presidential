using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public string[] tutorialText;
    public TextMeshProUGUI textBox;
    public float typingSpeed = 0.4f;

    private bool canContinueToNextLine;
    private int i = 0;

    void Start()
    {
        print("HI");
        StartCoroutine(DisplayText(tutorialText[i]));
        i++;
    }

    private IEnumerator DisplayText(string text)
    {
        print("IH");
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        // display 1 letter at a time
        foreach (char letter in text.ToCharArray())
        {
            print("HEY");
            textBox.GetComponent<TextMeshProUGUI>().text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        Invoke("enableTyping", typingSpeed * 5);
    }

    private void enableTyping()
    {
        StartCoroutine(DisplayText(tutorialText[i]));
        i++;
    }
}
