using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowItem : MonoBehaviour
{
    public GameObject Bow;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           Bow.SetActive(true);
                gameObject.SetActive(false);
            
        }
    }
}
