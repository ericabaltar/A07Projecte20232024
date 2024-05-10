using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTutorial : MonoBehaviour
{
    public TextMeshProUGUI textoUI; // Referencia al objeto de texto UI que quieres mostrar
    [SerializeField] private AudioClip ParchmentOpen;
    [SerializeField] private AudioClip ParchmentClose;
        private ControladorSonido controladorSonido;
    void Start()
    {
        // Al inicio, asegúrate de que el texto esté desactivado
        textoUI.enabled = false;
        controladorSonido = ControladorSonido.Instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entró en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Mostrar el texto UI
            textoUI.enabled = true;
            controladorSonido.EjecutadorDeSonido(ParchmentOpen);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto que salió del trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Ocultar el texto UI
            textoUI.enabled = false;
            controladorSonido.EjecutadorDeSonido(ParchmentClose);
        }
    }
}
