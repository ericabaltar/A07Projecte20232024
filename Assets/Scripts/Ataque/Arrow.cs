using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<OrcSystem>(out OrcSystem os))
        {
            StartCoroutine(os.GetDamage());
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
    }

}
