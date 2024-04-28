using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    public GameObject Prefab;

    public int numParticles = 10; // Changed nCubes to numParticles for clarity

    public float spawnRadius = 0.5f; // Radius for spawning around the enemy

    public float fillProbability = 0.9f;

    public Gradient ParticleGradient; // Assuming blood particles use a Gradient

    public float ColorSpeed = 1f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBloodParticles(); // Call the modified function
        }
    }

    public void SpawnBloodParticles() // Renamed for clarity
    {
        for (int i = 0; i < numParticles; i++)
        {
            if (Random.value < fillProbability) // Spawn based on probability
            {
                // Random position around the enemy
                Vector3 spawnPoint = transform.position + Random.insideUnitSphere * spawnRadius;
                spawnPoint.y = transform.position.y + 0.5f; // Keep Y position consistent

                // Random rotation
                Quaternion randomRot = Random.rotation;

                // Spawn particle with delayed destroy
                StartCoroutine(SpawnAndDestroyParticle(spawnPoint, randomRot));
            }
        }
    }

    IEnumerator SpawnAndDestroyParticle(Vector3 pos, Quaternion rot)
    {
        GameObject particle = Instantiate(Prefab, pos, rot);
        particle.transform.localScale *= Random.Range(0.1f, 1f);

        Color color = ParticleGradient.Evaluate(Mathf.Sin(Time.time * ColorSpeed) / 2 + 0.5f);
        particle.GetComponent<MeshRenderer>().material.color = color;

        // Wait for 2 seconds and then destroy the particle
        yield return new WaitForSeconds(2.0f);
        Destroy(particle);
    }
}