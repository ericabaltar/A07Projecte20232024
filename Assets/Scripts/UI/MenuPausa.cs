using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;

    private bool juegoPausado = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (juegoPausado)
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
        juegoPausado = true;
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el tiempo del juego
    }

    public void DesactivarMenuPausa()
    {
     juegoPausado = false;
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
    }

    public void EmpezarJuego()
    {
        
        DesactivarMenuPausa();
    }

    public void IrAMenuOpciones()
    {
        SceneManager.LoadScene("SettingsMenú"); 
    }

    public void SalirDelJuego()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}

