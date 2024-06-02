using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akey : MonoBehaviour
{
    public GameObject key;
    public GameObject Player;
    public string playername;
    public GameObject door;
    [SerializeField] private AudioClip colectar1; 
    // Start is called before the first frame update
    void Start()
    {
        playername = Player.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == playername)
        {
            ControladorSonido.Instance.EjecutadorDeSonido(colectar1);
            //
            Destroy(key);
            //Destroy(door);
        }
       
    }
}
