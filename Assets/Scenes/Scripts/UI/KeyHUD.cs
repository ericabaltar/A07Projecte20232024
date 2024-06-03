using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyHUD : MonoBehaviour
{

    public MovePJ MovePJ;

    public TextMeshProUGUI TextMeshProUGUI;

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI.text= MovePJ.Keys.ToString();
    }
}
