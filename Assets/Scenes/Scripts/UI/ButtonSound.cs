using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource audioSource;

    void Start()
    {
        // Obtén el componente AudioSource adjunto al botón
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Si no hay un AudioSource adjunto al botón, muestra un mensaje de advertencia
            Debug.LogWarning("No se encontró el componente AudioSource en el botón.");
        }
    }

    // Este metodo se llama cuando el cursor entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Si hay un AudioSource adjunto al botón, reproduce el sonido
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}

