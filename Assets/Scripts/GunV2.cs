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

    public Transform defaultPosition;
    public Transform aimingPosition;
    private Transform weaponPosition;
    public float aimingSpeed = 10f;
    public float defaultFOV = 75f;
    public float aimingFOV = 40f;
    private bool isAiming = false;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    private InputController inputController;

    public Animator animator;

    private void Start()
    {
        inputController = GetComponent<InputController>();

        weaponPosition = transform;

        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (inputController.Shoot && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetMouseButton(1))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        float t = isAiming ? 1f : 0f; 
        Vector3 targetPosition = isAiming ? aimingPosition.position : defaultPosition.position;
        weaponPosition.position = Vector3.Lerp(weaponPosition.position, targetPosition, aimingSpeed * Time.deltaTime);
        transform.position = weaponPosition.position;

        SetFieldOfView(Mathf.Lerp(fpsCam.fieldOfView, isAiming ? aimingFOV : defaultFOV, aimingSpeed * Time.deltaTime));
    }

    void SetFieldOfView(float fov)
    {
        fpsCam.fieldOfView = fov;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        bullet.Init(FireSpeed);

        //particulas.Play();
        //GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //Destroy(impact, 2f);

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(25);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime -.25f);

        animator.SetBool("isReloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
