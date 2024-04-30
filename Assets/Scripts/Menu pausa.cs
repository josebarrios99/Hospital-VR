using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public bool pause;
    public GameObject pauseMenu;
    public GameObject jugador;

    private void Start()
    {
        Time.timeScale = 1;
        pause = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause == false) {
                Pause();
            }
        }

    }
    public void Pause()
    {
        jugador.GetComponent<PlayerController>().CantMove = true;
        pause = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Resume()
    {
        jugador.GetComponent<PlayerController>().CantMove = false;
        pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
