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

    // Start is called before the first frame update
    void Start()
    {
        Door1.SetActive(false);
        Door2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
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
    }
}