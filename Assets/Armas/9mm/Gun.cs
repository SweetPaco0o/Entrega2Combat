using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet BulletPrefab;
    public Transform FirePoint;
    public float FireSpeed = 20f;

    public float RechargeTime = 0.5f;
    private float _lastShot;

    public float BeamLength = 20f;

    public LayerMask WhatIsShootable;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (CanShoot())
            Shoot();
    }

    private bool CanShoot()
    {
        return (_lastShot + RechargeTime) < Time.time;
    }

    private void Shoot()
    {
        RaycastHit hit;
        Vector3 endPos;

        Bullet bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        bullet.Init(FireSpeed);
        _lastShot = Time.time;

        if (Physics.Raycast(FirePoint.position, FirePoint.forward, out hit, 10, WhatIsShootable))
        {
            endPos = hit.point;
            Debug.Log("Shoot at " + hit.transform.name);
        }
        else
        {
            endPos = transform.position + FirePoint.forward * BeamLength;
        }
    }
}
