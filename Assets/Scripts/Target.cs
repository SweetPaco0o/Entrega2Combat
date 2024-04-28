using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Color originalColor;
    public Color damageColor;
    public float damageFlashTime = 0.2f;

    private Material myMaterial; // Using myMaterial for clarity

    void Start()
    {
        currentHealth = maxHealth;

        // Try to get the Mesh Renderer on this GameObject
        myMaterial = GetComponent<MeshRenderer>()?.material;

        // Check if material was found
        if (myMaterial == null)
        {
            Debug.LogWarning("Target: No MeshRenderer found!");
            // Handle the case where there's no MeshRenderer (optional)
        }
        else
        {
            originalColor = myMaterial.color;
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Infligiendo daño al enemigo.");
        if (currentHealth <= 0)
        {
            Debug.Log("matando.");
            Die();
        }
        else
        {
            // Flash damage color if material exists
            if (myMaterial != null)
            {
                StartCoroutine(DamageFlash());
            }
        }
    }

    private IEnumerator DamageFlash()
    {
        myMaterial.color = damageColor;
        yield return new WaitForSeconds(damageFlashTime);
        myMaterial.color = originalColor;
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        // Destruir recursivamente el GameObject y todos sus hijos
        DestroyRecursive(gameObject);
    }

    private void DestroyRecursive(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            DestroyRecursive(child.gameObject);
        }
        Destroy(obj);
    }
}