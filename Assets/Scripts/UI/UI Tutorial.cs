using UnityEngine;

public class UITutorial : MonoBehaviour
{
    public Sprite[] controlSprites; // Array que contiene las dos imágenes de los controles
    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private bool playerInside = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Desactivar la animación al inicio
        spriteRenderer.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entró en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            // Activar la animación cuando el jugador entre en el trigger
            spriteRenderer.enabled = true;
            // Comenzar la animación intercalada
            InvokeRepeating("ToggleControlSprite", 0f, 0.5f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto que salió del trigger es el jugador
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            // Desactivar la animación cuando el jugador salga del trigger
            spriteRenderer.enabled = false;
            // Detener la animación intercalada
            CancelInvoke("ToggleControlSprite");
            // Restaurar el sprite por defecto cuando el jugador sale del área del trigger
            spriteRenderer.sprite = null;
        }
    }

    void ToggleControlSprite()
    {
        // Mostrar los sprites solo si el jugador está dentro del trigger
        if (playerInside)
        {
            // Cambiar el sprite del objeto entre las dos imágenes
            spriteRenderer.sprite = controlSprites[currentIndex];
            // Alternar entre las dos imágenes
            currentIndex = (currentIndex + 1) % controlSprites.Length;
        }
    }
}
