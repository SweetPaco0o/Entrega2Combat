using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;

    public Camera fpsCam;
    public Animator animator;
    private InputController inputController;
    //public GameObject hitEffect;

    bool attacking = false;
    bool readytoAttack = true;
    int attackCount;

    void Start()
    {
        inputController = GetComponent<InputController>();
    }

    void Update()
    {
        if(inputController.Shoot)
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        GetComponent<AudioSource>().Play();

        if (!readytoAttack || attacking)
        {
            return;
        }

        readytoAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        if (attackCount == 0)
        {
            animator.SetBool("Attack1", true);
            attackCount++;
        }
        else
        {
            animator.SetBool("Attack2", true);
            attackCount = 0;
        }
        
    }

    void ResetAttack()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        readytoAttack = true;
        attacking = false;
    }
    void AttackRaycast()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, attackDistance))
        {
            //HitTarget(hit.point);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(50);
            }
        }
    }
    /*void HitTarget(Vector3 pos)
    {
        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }*/
}