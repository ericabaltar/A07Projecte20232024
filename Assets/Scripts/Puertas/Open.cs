using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Door1;
    public GameObject player;
    public GameObject Area;

    // Start is called before the first frame update
    void Start()
    {

        Door1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Door1.SetActive(true);
    }

    void SetActive()
    {
        Door1.SetActive(!Door1.activeSelf);
    }
}
