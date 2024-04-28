using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform FirePoint;

    public float BeamLength = 200f;

    public LayerMask WhatIsShootable;

    public LineRenderer lineRenderer;
    private bool isShooting = false;

    private InputController inputController;

    private void Start()
    {
        inputController = GetComponent<InputController>();
    }

    void Update()
    {
        if (inputController.Shoot && !isShooting)
        {
            isShooting = true;
            Shoot();
        }

        if (!inputController.Shoot && isShooting)
        {
            isShooting = false;
            lineRenderer.enabled = false;
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        Vector3 endPos;

        // Crear un rayo desde la posición de la cámara hacia adelante
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, BeamLength, WhatIsShootable))
        {
            endPos = hit.point;
            Debug.Log("Shoot at " + hit.transform.name);
            Destroy(hit.transform.gameObject);
        }
        else
        {
            // Si no se detecta un objeto, el final del rayo está en la dirección del rayo con una longitud fija
            endPos = ray.GetPoint(BeamLength);
        }

        lineRenderer.SetPosition(0, FirePoint.position);
        lineRenderer.SetPosition(1, endPos);
        lineRenderer.enabled = true;
    }
}