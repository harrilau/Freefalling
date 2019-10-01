using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;

    public Text text;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endLine;

    public PlayerController player;

    public bool isActive;

    public bool stopPlayerMovement;

    private bool isTyping = false;
    public bool cancelTyping = false;

    public float typeSpeed;

    public bool startGameWhenDone;
    public bool endGameWhenDone;
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endLine == 0)
        {
            endLine = textLines.Length - 1;
        }

        if (isActive)
            EnableTextBox();
        else
            DisableTextBox();
    }

    void Update()
    {
        if (!isActive)
            return;

        //text.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine++;

                if (currentLine > endLine) { 
                    if (endGameWhenDone)
                    {
                        SceneManager.LoadScene("Main Menu");
                    } else if (startGameWhenDone)
                    {
                        SceneManager.LoadScene("Desert 1");
                    }
                    DisableTextBox();
                }

                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    private IEnumerator TextScroll (string lineOfText)
    {
        int letter = 0;
        text.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length-1))
        {
            text.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }
        text.text = lineOfText; //if cancelTyping becomes true at any point
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;

        if (stopPlayerMovement)
            player.canMove = false;

        StartCoroutine(TextScroll(textLines[currentLine]));
    }

    public void DisableTextBox()
    {
        
        textBox.SetActive(false);
        isActive = false;
        player.canMove = true;
    }

    public void ReloadScript(TextAsset newText)
    {
        if (newText != null)
        {
            textLines = new string[1];
            textLines = (newText.text.Split('\n'));
        }
    }
}
