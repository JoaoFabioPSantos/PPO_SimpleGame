using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBenta : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject activeSignal;
    public GameObject backgroundDialogue;
    public string[] dialogue;
    public int index;
    public bool dialogueGoing = true;

    public float textSpeed;
    public bool playerIsClosed;

    public LayerMask playerLayer;
    public float radious;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClosed && dialogueGoing)
        {
            zeroText();
                if (dialoguePanel.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    dialoguePanel.SetActive(true);
                    backgroundDialogue.SetActive(true);
                    StartCoroutine(Typing());
                }
 
        }
    }

    private void FixedUpdate()
    {
        Interact();
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

        if (hit != null)
        {
            activeSignal.SetActive(true);
            playerIsClosed = true;
        }
        else
        {
            playerIsClosed = false;
            activeSignal.SetActive(false);
            zeroText();
            dialogueGoing = true;
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        backgroundDialogue.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
            dialogueGoing = false;
            NextLine();
        }
        dialogueGoing = true;
    }

    public void NextLine()
    {
       if (dialogueText.text == dialogue[index])
        {
            if (index < dialogue.Length - 1)
            {
                index++;
                dialogueText.text = "";
                dialogueGoing = false;
                StartCoroutine(Typing());
            }
            else
            {
                zeroText();
            }
        }
    }

}