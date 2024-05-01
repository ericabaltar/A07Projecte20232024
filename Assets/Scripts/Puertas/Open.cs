using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject player;
    public GameObject Area;

    // Start is called before the first frame update
    void Start()
    {
        Door1.SetActive(false);
        Door2.SetActive(false);
        Enemy.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Door1.SetActive(true);
        Door2.SetActive(true);

        if (Enemy = null)
        {
            Door1.SetActive(false);
            Door2.SetActive(false);
        }
    }

    void SetActive()
    {
        Door1.SetActive(!Door1.activeSelf);
        Door2.SetActive(!Door1.activeSelf);
    }
}