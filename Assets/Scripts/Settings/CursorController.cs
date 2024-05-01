using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D defaultCursorTexture; // Textura del cursor por defecto
    public Texture2D clickedCursorTexture; // Textura del cursor al hacer clic
    public Texture2D enemyCursorTexture; // Textura del cursor cuando est� sobre un enemigo
    public Vector2 cursorHotspot = Vector2.zero; // Punto caliente del cursor (centro por defecto)

    private bool isCursorOverEnemy = false; // Variable para rastrear si el cursor est� sobre un enemigo o jefe

    void Start()
    {
        // Si se proporciona una textura de cursor personalizado, asignarla
        if (defaultCursorTexture != null)
        {
            Cursor.SetCursor(defaultCursorTexture, cursorHotspot, CursorMode.Auto);
        }
    }

    void Update()
    {
        // Obtener la posici�n del cursor del mouse
        Vector3 cursorScreenPosition = Input.mousePosition;

        // Convertir la posici�n del cursor a la esquina superior izquierda de la pantalla
        cursorScreenPosition.z = 10f; // Asegurarse de que el cursor est� en el frente de la c�mara
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(cursorScreenPosition);
        transform.position = new Vector3(cursorWorldPosition.x, cursorWorldPosition.y, 0f);

        // Si se hace clic, cambiar el cursor a la textura de clic y luego volver al cursor predeterminado
        if (Input.GetMouseButtonDown(0) && clickedCursorTexture != null)
        {
            Cursor.SetCursor(clickedCursorTexture, cursorHotspot, CursorMode.Auto);
            Invoke("RestoreDefaultCursor", 0.1f); // Llamar a la funci�n para restaurar el cursor predeterminado despu�s de un peque�o retraso
        }

        // Detectar si el cursor est� sobre un GameObject con el tag "enemy" o "boss"
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Boss")))
        {
            // Cambiar el cursor a la textura de enemigo si est� sobre un enemigo o jefe
            if (enemyCursorTexture != null)
            {
                Cursor.SetCursor(enemyCursorTexture, cursorHotspot, CursorMode.Auto);
                isCursorOverEnemy = true;
            }
        }
        else
        {
            // Si no est� sobre un enemigo, restablecer la bandera y la textura del cursor
            if (isCursorOverEnemy)
            {
                Cursor.SetCursor(defaultCursorTexture, cursorHotspot, CursorMode.Auto);
                isCursorOverEnemy = false;
            }
        }
    }

    // Funci�n para restaurar el cursor predeterminado
    void RestoreDefaultCursor()
    {
        if (defaultCursorTexture != null)
        {
            Cursor.SetCursor(defaultCursorTexture, cursorHotspot, CursorMode.Auto);
        }
    }
}
