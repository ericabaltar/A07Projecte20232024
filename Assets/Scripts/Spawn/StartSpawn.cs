using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawn : MonoBehaviour
{
    NewBehaviourScript1 spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GetComponent<NewBehaviourScript1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spawn.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spawn.enabled = false;
    }
}
