using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void RetryGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Settings()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    
    }
}
