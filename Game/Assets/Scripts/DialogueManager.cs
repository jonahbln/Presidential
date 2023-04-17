using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public string[] dialogueText;
    public TextMeshProUGUI textBox;
    public float typingDelay = 0.05f;
    public float lineDelay = 5f;
    public AudioSource typeSFX;

    private bool canContinueToNextLine;
    private int i = 0;
    private bool gameOver = false;

    void Start()
    {
        StartCoroutine(DisplayText(dialogueText[i]));
        i++;
        LevelManager.gamePaused = true;
    }

    private IEnumerator DisplayText(string text)
    {
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        typeSFX.Play();

        // display 1 letter at a time
        foreach (char letter in text.ToCharArray())
        {
            textBox.GetComponent<TextMeshProUGUI>().text += letter;
            yield return new WaitForSeconds(typingDelay);
        }

        typeSFX.Pause();

        Invoke("enableTyping", lineDelay);
    }

    private void enableTyping()
    {
        if(i >= dialogueText.Length)
        {
            LevelManager.gamePaused = false;
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if(!gameOver)
            {
                PresidentBehavior.transformation = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                SceneManager.LoadScene(0);
            }

        }
        else
        {
            StartCoroutine(DisplayText(dialogueText[i]));
            i++;
        }
    }

    public void GameOver()
    {
        LevelManager.gamePaused = true;
        dialogueText[0] = "AGHHHHHHHHH NOOOOOOOO!";
        dialogueText[1] = "You will never beat the balliens!";
        dialogueText[2] = "We will only come back stronger than ever!";
        dialogueText[3] = "I will see you again!";
        i = 0;
        StartCoroutine(DisplayText(dialogueText[i]));
    }
}
