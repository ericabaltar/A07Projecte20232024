using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float speed = 3;

    private bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<RangeEnemy>(out RangeEnemy os))
        {
            StartCoroutine(os.GetDamage());
            Destroy(gameObject);
        }
        else if(collision.gameObject)
        {
            hasCollided = true;
            Destroy(this.gameObject,3f);
        }
        this.GetComponent<BoxCollider2D>().enabled = false;
        rigidbody.bodyType = RigidbodyType2D.Static;
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
