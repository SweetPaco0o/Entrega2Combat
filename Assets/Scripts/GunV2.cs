using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class GunV2 : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 50f;
    public float impactForce = 30f;

    public Camera fpsCam;
    //public ParticleSystem particulas;
    //public GameObject impactEffect;

    private float timeToFire = 0f;

    public Bullet BulletPrefab;
    public Transform FirePoint;
    public float FireSpeed = 200f;

    private InputController inputController;

    private void Start()
    {
        inputController = GetComponent<InputController>();
    }

    void Update()
    {
        if(inputController.Shoot && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        bullet.Init(FireSpeed);

        //particulas.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impact, 2f);
        }
    }
}
