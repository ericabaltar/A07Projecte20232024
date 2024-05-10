using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaOrco : MonoBehaviour
{
    [SerializeField] private Image ImagenBarraOrco;

    public void UpdateHealthBar(float maxHealth, float health)
    {
        ImagenBarraOrco.fillAmount = health / maxHealth;
    }
}
