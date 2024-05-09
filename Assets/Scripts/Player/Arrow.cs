using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float speed = 3;
    public float lifetime = 0.5f; // Tiempo de vida de la flecha en segundos

    private bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyAfterDelay(lifetime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Si colisiona con un gameobject que tenga el tag "Enemy", se destruye
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasCollided)
        {
            rigidbody.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
        }
    }
}
