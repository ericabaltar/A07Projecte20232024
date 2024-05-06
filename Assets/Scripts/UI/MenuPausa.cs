using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (menuPausaUI.activeSelf)
            {
                DesactivarMenuPausa();
            }
            else
            {
                ActivarMenuPausa();
            }
        }
    }

    public void ActivarMenuPausa()
    {
        
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el tiempo del juego
    }

    public void DesactivarMenuPausa()
    {
     
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
    }

    public void EmpezarJuego()
    {
        SceneManager.LoadScene("Mysteria");
        DesactivarMenuPausa();
    }

    public void IrAMenuOpciones()
    {
        SceneManager.LoadScene("SettingsMenú"); 
    }

    public void SalirDelJuego()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}

