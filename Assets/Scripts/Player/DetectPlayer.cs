using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject player;
    public GameObject Area;
    // Start is called before the first frame update
    void Start()
    {

       Enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Enemy != null)
        {
            Enemy.SetActive(true);
        }
    }
}
