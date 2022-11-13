using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class DialogueManager : MonoBehaviour
{

    [Header("Components")]
    public GameObject dialogObject;
    public Text speechText;

    [Header("Settings")]
    public float typingSpeed;

    public void Speech(string txt)
    {
        dialogObject.SetActive(true);
        speechText.text = txt;
    }

}
