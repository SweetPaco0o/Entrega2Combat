using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Color originalColor;
    public Color damageColor;
    public float damageFlashTime = 0.2f;
    public Image deathImage;

    public HealthBar healthBar;

    private Material myMaterial;
    private float flashTimer;

    public bool isInvincible = false;

    public Transform Respawn;

    private void Start()
    {
        currentHealth = maxHealth;
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;

        healthBar.SetMaxHealth(maxHealth);
        deathImage.gameObject.SetActive(false);
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
        if (!isInvincible)
        {
            currentHealth -= damage;
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
        flashTimer = damageFlashTime;
        if (!isInvincible)
        {
            StartCoroutine(InvulnerabilityTimer());
        }
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator InvulnerabilityTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1.0f);
        isInvincible = false;
        Debug.Log("isInvincible");
    }

    private void Die()
    {
        transform.position = Respawn.position;
        Debug.Log(Respawn.position);
        deathImage.gameObject.SetActive(true);
        StartCoroutine(HideDeathImage());
        currentHealth = maxHealth;
    }

    IEnumerator HideDeathImage()
    {
        yield return new WaitForSeconds(2.0f);
        deathImage.gameObject.SetActive(false); 
    }
}
