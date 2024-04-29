using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            melee = !melee;
    }

    public void TomarDano(float dano)
    {
        vida -= dano;
        barraDeVida.CambiarVidaActual(vida);
        if (vida <= 0)
        {
            // Reiniciar la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Curar(float curacion)
    {
        vida += curacion; // Agrega la curación a la vida actual
        if (vida > maximoVida)
        {
            vida = maximoVida; // Limita la vida al máximo
        }
        barraDeVida.CambiarVidaActual(vida);
    }
}
