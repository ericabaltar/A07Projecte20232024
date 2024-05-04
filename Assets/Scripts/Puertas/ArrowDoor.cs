using UnityEngine;

public class ArrowDoor : MonoBehaviour
{
    public GameObject Door;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Door.SetActive(false);
        }
    }
}

