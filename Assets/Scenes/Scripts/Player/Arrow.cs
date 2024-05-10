using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private AudioSource audioSource;

    public float speed = 3;
    public float lifetime = 0.5f; // Tiempo de vida de la flecha en segundos
    public AudioClip appearSound; // Sonido al aparecer la flecha
    public AudioClip impactSound; // Sonido al impactar con alguna superficie

    private bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(DestroyAfterDelay(lifetime));

        if (appearSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(appearSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            if (impactSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(impactSound);
            }
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

