using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatePJ : MonoBehaviour
{
    [SerializeField] private float vida;

    [SerializeField] private float maximoVida;

    [SerializeField] private BarraDeVidaPJ barraDeVidaPJ;

    private void Start()
    {
        vida = maximoVida;
        barraDeVidaPJ.InicializarBarraVida(vida);
    }

    public void TomarDa�o(float da�o)
    {
        vida -= da�o;
        barraDeVidaPJ.CambiarVidaActual(vida);
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