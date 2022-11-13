using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public string scene;
    public GameObject optionsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(scene);
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void BackMenu()
    {
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        //Editor
        //UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }
}
