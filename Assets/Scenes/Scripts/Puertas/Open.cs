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
    [SerializeField] private AudioClip abrir;
    [SerializeField] private AudioClip cerrar;
    [SerializeField] private AudioClip musicaBatalla;
    private AudioSource batallaAudioSource; // Para la m?sica de batalla
    private Collider2D triggerCollider; // Referencia al collider del trigger

    private void Start()
    {
        Door1.SetActive(false);
        Door2.SetActive(false);
        triggerCollider = GetComponent<Collider2D>(); // Obtener el collider del trigger

        // A?adir dos componentes AudioSource para reproducir la m?sica de ambiente y la m?sica de batalla
        batallaAudioSource = gameObject.AddComponent<AudioSource>();
        batallaAudioSource.clip = musicaBatalla;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            // Reproduce el sonido de abrir la puerta
            ControladorSonido.Instance.EjecutadorDeSonido(abrir);
            OpenDoors();


            batallaAudioSource.Play();

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
                // Reproduce el sonido de cerrar la puerta
                ControladorSonido.Instance.EjecutadorDeSonido(cerrar);
                CloseDoors();


                batallaAudioSource.Stop();

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