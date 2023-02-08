using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        //Load de la scene à l'index n-1 car la scene du jeu est à l'index 0:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);             
    }

    public void QuitGame()
    {
        // Pour quitter le jeu:
        //Application.Quit();
        Debug.Log("Player has quit the game");
    }
}
