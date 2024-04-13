using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rigidbody;
    // Start is called before the first frame update
    private void Start()
    {
        //Destroy(gameObject, 5);
    }
    public void Init(float speed)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
