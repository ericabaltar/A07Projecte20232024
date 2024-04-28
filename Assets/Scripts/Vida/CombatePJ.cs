
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CombatePJ : MonoBehaviour
{
    [SerializeField] private float vida;

    [SerializeField] private float maximoVida;

    [SerializeField] private BarraDeVida barraDeVida;


    public bool melee = true;

    private void Start()
    {
        vida = maximoVida;
    }
    private void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.Space))
            melee = !melee;
    }

    public void TomarDano(float dano)
    {
        vida -= dano;
        barraDeVida.CambiarVidaActual(vida);
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Curar(float curacion)
    {
        if ((vida + curacion) > maximoVida)
        {
            vida = maximoVida;
        }
    }
}