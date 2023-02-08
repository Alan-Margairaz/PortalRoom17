using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    // Variables récupération d'inputs d'UI:
    private PlayerControls playerControls;

    // Variables pour la mise en pause: 
    public static bool isPaused = false;
    public GameObject pauseMenuCanvas;


    private void Awake()
    {
        // Initialisation de l'Input System:
        playerControls = new PlayerControls();

        // Initialisation du time scale utilisé pour freeze le jeu lorsque l'on met pause:
        Time.timeScale = 1f;
    }

    private void Update()
    {
        Play();
    }

    public void Play()
    {
        if (playerControls.UI.Pause.triggered)
        {
            if (isPaused)
            {
                Debug.Log("Game unpaused");

                // Lorsque l'on appuie sur Tab et que le jeu est sur pause, on "dé-freeze" le jeu et on désactive la menu:
                pauseMenuCanvas.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;       // On re-lock le curseur pour le gameplay
            }
            else
            {
                Debug.Log("Game paused");

                // Lorsque l'on appuie sur Tab et que le jeu n'est pas sur pause, on freeze le jeu et on active la menu:
                pauseMenuCanvas.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
                Cursor.lockState = CursorLockMode.None;     // On libère le curseur pour naviguer dans le menu
            }
        }
    }

    public void MainMenuButton()
    {
        // Permet de reload la scene qui contient l'UI du menu principal:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartLevel()
    {
        // Reload de la scene complète:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;            // Evite le freeze du jeu lors du restart
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();    
    }
}

