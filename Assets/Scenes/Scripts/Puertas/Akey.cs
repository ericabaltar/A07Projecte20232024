using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akey : MonoBehaviour
{
    public GameObject key;
    public GameObject Player;
    public MovePJ movePJ;  // Referencia al script MovePJ
    public GameObject door;
    [SerializeField] private AudioClip colectar1;

    void Start()
    {
        movePJ = Player.GetComponent<MovePJ>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject == Player)
        {
            ControladorSonido.Instance.EjecutadorDeSonido(colectar1);
            movePJ.Keys++;
            Destroy(key);
            Debug.Log("Llave recogida. Llaves actuales: " + movePJ.Keys);
        }
    }
}
