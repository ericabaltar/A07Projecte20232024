using UnityEngine;

public class ArrowDoor : MonoBehaviour
{
    public GameObject Door;
    [SerializeField] private AudioClip colectar1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            ControladorSonido.Instance.EjecutadorDeSonido(colectar1);
            Door.SetActive(false);
        }
    }
}

