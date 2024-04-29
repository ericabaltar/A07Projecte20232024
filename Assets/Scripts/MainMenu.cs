using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public GameObject settingsPanel;
  
   public void StartGame()
   {
        SceneManager.LoadScene(2);
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


   public void ExitGame()
   {
        Debug.Log("Exiting");
        Application.Quit();
   }

}
