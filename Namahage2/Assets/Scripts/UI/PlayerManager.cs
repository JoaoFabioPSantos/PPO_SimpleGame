using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public bool win;
    [SerializeField] Rigidbody2D player;


    [Header("Menu e Pause")]
    private bool isPaused;
    public GameObject pausePanel;
    public string cena;

    void Start()
    {
        win = false;
    }

    void Update()
    {
        winGame();
    }


    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player") == true)
        {
            win = true;
        }
    }

    private void winGame()
    {
        if (win == true)
        {
           player.constraints = RigidbodyConstraints2D.FreezeAll;
           WinScreen();
        }
    }

    void WinScreen()
    {
        if (!win)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(cena);
    }
}
