using UnityEngine;

public class ArrowDoor : MonoBehaviour
{
    public GameObject Door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            Door.SetActive(false);
        }
    }
}

