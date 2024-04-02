using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatePJ : MonoBehaviour
{
    [SerializeField] private float vida;

    [SerializeField] private float maximoVida;

    [SerializeField] private BarraDeVida barraDeVida;

    private void Start()
    {
        vida = maximoVida;
        barraDeVida.InicializarBarraVida(vida);
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