using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelaodBar : MonoBehaviour
{
    public Image reloadFill;
    float reloadTime = 2f; // Tiempo de recarga en segundos
    float currentFillAmount = 0f; // Valor actual de relleno
    float targetFillAmount = 1f; // Valor objetivo de relleno

    void Start()
    {
        // Inicializa el valor de relleno de la imagen de recarga
        reloadFill.fillAmount = currentFillAmount;
    }

    void Update()
    {
        // Si el valor actual de relleno no ha alcanzado el valor objetivo
        if (currentFillAmount < targetFillAmount)
        {
            // Calcula la nueva cantidad de relleno utilizando una interpolación lineal
            currentFillAmount += Time.deltaTime / reloadTime;
            // Asegura que el valor actual no supere el valor objetivo
            currentFillAmount = Mathf.Clamp01(currentFillAmount);
            // Aplica el nuevo valor de relleno a la imagen de recarga
            reloadFill.fillAmount = currentFillAmount;
        }
    }
}
