using UnityEngine;

public class UITutorial : MonoBehaviour
{
    public Sprite[] controlSprites; // Array que contiene las dos im�genes de los controles
    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private bool playerInside = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Desactivar la animaci�n al inicio
        spriteRenderer.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entr� en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            // Activar la animaci�n cuando el jugador entre en el trigger
            spriteRenderer.enabled = true;
            // Comenzar la animaci�n intercalada
            InvokeRepeating("ToggleControlSprite", 0f, 0.5f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto que sali� del trigger es el jugador
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            // Desactivar la animaci�n cuando el jugador salga del trigger
            spriteRenderer.enabled = false;
            // Detener la animaci�n intercalada
            CancelInvoke("ToggleControlSprite");
            // Restaurar el sprite por defecto cuando el jugador sale del �rea del trigger
            spriteRenderer.sprite = null;
        }
    }

    void ToggleControlSprite()
    {
        // Mostrar los sprites solo si el jugador est� dentro del trigger
        if (playerInside)
        {
            // Cambiar el sprite del objeto entre las dos im�genes
            spriteRenderer.sprite = controlSprites[currentIndex];
            // Alternar entre las dos im�genes
            currentIndex = (currentIndex + 1) % controlSprites.Length;
        }
    }
}
