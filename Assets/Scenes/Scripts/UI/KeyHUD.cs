using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHUD : MonoBehaviour
{
    public MovePJ MovePJ;
    public Image KeyImage;  

    // Update is called once per frame
    void Update()
    {
        
        KeyImage.enabled = MovePJ.Keys > 0;
    }
}

