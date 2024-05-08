using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void Settings()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;

    }
}
