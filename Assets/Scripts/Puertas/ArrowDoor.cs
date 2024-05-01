using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDoor : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public GameObject Door;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.GameObject.CompareTag("Arrow")){
           GameObject.setActive(false);
        }
    }
}