using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akey : MonoBehaviour
{
    public GameObject key;
    public GameObject Player;
    public string playername;
    public bool KeyIsOn;
    // Start is called before the first frame update
    void Start()
    {
        playername = Player.name;

        KeyIsOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == playername)
        //
        Destroy(key);
        KeyIsOn = false;
    }
}
