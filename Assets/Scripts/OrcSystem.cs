using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class OrcSystem : MonoBehaviour
{
    [SerializeField] private BarraDeVidaOrco barraDeVidaOrco;
    [SerializeField] private float maxHealth;

    private float health;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        health = maxHealth;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnMouseDown()
    {
        StartCoroutine(GetDamage());
    }
   
    IEnumerator GetDamage()
    {
        float damageDuration = 0.1f;
        float damage = UnityEngine.Random.Range(1f, 5f);
        health -= damage;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);

        if (health > 0)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(damageDuration);
            spriteRenderer.color = Color.white;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
