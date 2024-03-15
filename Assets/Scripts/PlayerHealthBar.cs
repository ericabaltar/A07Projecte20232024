using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    public Image playerHealthBar;

    public float currHealth;
    public float maxHealth;


    // Update is called once per frame
    void Update()
    {
        playerHealthBar.fillAmount = currHealth / maxHealth;
    }
}
