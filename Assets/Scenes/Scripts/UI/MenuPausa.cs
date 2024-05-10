using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;

    public static bool IsPaused;
    void Start()
    {
       menuPausaUI.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el tiempo del juego
    }

    public void ResumeGame()
    {
        IsPaused= false;
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
    }

}

