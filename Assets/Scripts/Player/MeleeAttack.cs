using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    private Animator animator;
    private BoxCollider2D hitbox;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        hitbox = transform.Find("Hitbox").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0) && TryGetComponent<CombatePJ>(out CombatePJ cpj)) // Attack on Space key press.
        {
            if (cpj.melee)
            {
                animator.SetTrigger("MeleeAttack");
                Invoke("ActivateHitbox", 0.2f); // Activate hitbox after 0.2 seconds.
                Invoke("DeactivateHitbox", 0.4f); // Deactivate hitbox after 0.4 seconds.

                // Debug log cuando se realiza un ataque melee
                Debug.Log("El jugador ha realizado un ataque a melee");
            }

        }
    }
    void ActivateHitbox()
    {
        hitbox.gameObject.SetActive(true);
    }

    void DeactivateHitbox()
    {
        hitbox.gameObject.SetActive(false);
    }
}
