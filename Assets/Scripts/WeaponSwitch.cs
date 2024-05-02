using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;

    private InputController inputController;

    public TextMeshProUGUI BulletCount;
    public Image Bullet;
    public Image ReloadDefault;
    public Image ReloadFill;

    public Animator animator;

    void Start()
    {
        SelectWeapon();
        inputController = GetComponent<InputController>();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapon >= transform.childCount - 1) 
            {
                selectedWeapon = 0;
            } else
            {
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if(inputController.Arma1)
        {
            selectedWeapon = 0;
        }

        if (inputController.Arma2 && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        if (inputController.Arma3 && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        if (selectedWeapon == 0)
        {
            BulletCount.gameObject.SetActive(true);
            Bullet.gameObject.SetActive(true);
        }
        else
        {
            BulletCount.gameObject.SetActive(false);
            Bullet.gameObject.SetActive(false);
            ReloadDefault.gameObject.SetActive(false);
            ReloadFill.gameObject.SetActive(false);
            animator.SetBool("isReloading", false);
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon){
                weapon.gameObject.SetActive(true);
            }
            else{
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
