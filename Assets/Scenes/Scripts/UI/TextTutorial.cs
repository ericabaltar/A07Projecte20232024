using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTutorial : MonoBehaviour
{
    public TextMeshProUGUI textoUI; // Referencia al objeto de texto UI que quieres mostrar

    void Start()
    {
        // Al inicio, aseg�rate de que el texto est� desactivado
        textoUI.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entr� en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Mostrar el texto UI
            textoUI.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto que sali� del trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Ocultar el texto UI
            textoUI.enabled = false;
        }
    }
}
