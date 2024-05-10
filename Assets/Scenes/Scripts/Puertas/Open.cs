using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    private bool seHaEjecutado = false;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject player;
    public GameObject Area;
    private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private AudioClip Abrir;
    [SerializeField] private AudioClip Cerrar;
    private Collider2D triggerCollider; // Referencia al collider del trigger

    // Start is called before the first frame update
    void Start()
    {
        Door1.SetActive(false);
        Door2.SetActive(false);
        triggerCollider = GetComponent<Collider2D>(); // Obtener el collider del trigger
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            ControladorSonido.Instance.EjecutadorDeSonido(Abrir);
            OpenDoors();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            enemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(collision.gameObject);
            if (enemies.Count == 0)
            {
                ControladorSonido.Instance.EjecutadorDeSonido(Cerrar);
                CloseDoors();
            }
        }
    }

    private void OpenDoors()
    {
        if (!seHaEjecutado)
        {
            Door1.SetActive(true);
            Door2.SetActive(true);
            seHaEjecutado = true;
        }
    }

    private void CloseDoors()
    {
        Destroy(Door1);
        Destroy(Door2);
        Destroy(triggerCollider); // Destruir el collider del trigger junto con la puerta
    }
}
