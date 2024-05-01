using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAbility : MonoBehaviour
{
    public GameObject Doors;
    public GameObject Ability;
    public GameObject Area;

    // Start is called before the first frame update
    void Start()
    {

        Doors.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Doors.SetActive(false);
    }

    void SetActive()
    {
        Doors.SetActive(!Doors.activeSelf);
    }
}
