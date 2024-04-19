using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Color originalColor;
    public Color damageColor;
    public float damageFlashTime = 0.2f;

    public HealthBar healthBar;

    private Material myMaterial;
    private float flashTimer;

    public bool isInvincible = false;

    private void Start()
    {
        currentHealth = maxHealth;
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;

        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerTakesDamage(25);
        }
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            myMaterial.color = damageColor;
        }
        else
        {
            myMaterial.color = originalColor;
        }
    }

    public void PlayerTakesDamage(int damage)
    {
        if ((!isInvincible))
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
            flashTimer = damageFlashTime;
            StartCoroutine(InvulnerabilityTimer());            
        }
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator InvulnerabilityTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(5.0f);
        isInvincible = false;
        Debug.Log("isInvincible");
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
