using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Color originalColor;
    public Color damageColor;
    public float damageFlashTime = 0.2f;

    private Material childMaterial; // Using childMaterial for clarity

    void Start()
    {
        currentHealth = maxHealth;

        // Try to get the first child's Renderer
        Transform childTransform = transform.GetChild(0);
        if (childTransform != null)
        {
            childMaterial = childTransform.GetComponent<Renderer>()?.material;
        }

        // Check if child material was found
        if (childMaterial == null)
        {
            Debug.LogWarning("EnemyHealthSystem: No child with Renderer found!");
            // Handle the case where there's no child renderer (optional)
        }
        else
        {
            originalColor = childMaterial.color;
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
            // Flash damage color if child material exists
            if (childMaterial != null)
            {
                StartCoroutine(DamageFlash());
            }
        }
    }

    private IEnumerator DamageFlash()
    {
        childMaterial.color = damageColor;
        yield return new WaitForSeconds(damageFlashTime);
        childMaterial.color = originalColor;
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