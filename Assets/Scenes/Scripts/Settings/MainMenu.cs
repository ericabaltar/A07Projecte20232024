using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public GameObject settingsPanel;
    public AudioMixer mixer;

   public void StartGame()
   {

        SceneManager.LoadScene("CutScene");
    }

   public void OpenSettings()
   {

        //settingsPanel.SetActive(true);
        SceneManager.LoadScene(1);
   }

   public void CloseSettings()
   {

        //settingsPanel.SetActive(false);
        SceneManager.LoadScene(0);
   }

   public void OpenCredits()
    {
  
        //settingsPanel.SetActive(true);
        SceneManager.LoadScene(5);
    }

   public void CloseCredits()
    {

        //settingsPanel.SetActive(false);
        SceneManager.LoadScene(0);

    }

   public void ExitGame()
   {

        Debug.Log("Exiting");
        Application.Quit();
   }

}
