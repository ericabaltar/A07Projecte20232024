using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el jugador entra en contacto con la poción
        if (other.CompareTag("Player"))
        {
            // Obtener el componente de salud del jugador
            HealthBehaviour playerHealth = other.GetComponent<HealthBehaviour>();

            // Verificar si el jugador tiene el componente de salud
            if (playerHealth != null)
            {
                // Restaurar la salud al máximo
                playerHealth.RestoreMaxHealth();

                // Desactivar la poción para que no pueda ser recogida nuevamente
                gameObject.SetActive(false);
            }
        }
    }
}

