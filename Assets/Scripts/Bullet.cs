using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rigidbody;

    private void Start()
    {
        //Destroy(gameObject, 5f);
    }
    public void Init(float speed)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealthSystem enemyHealth = collision.gameObject.GetComponent<EnemyHealthSystem>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(100);
        }

        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
