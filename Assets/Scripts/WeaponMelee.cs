using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : WeaponBase
{
    DamageCollider damageCollider;
    Animator animator;
    bool canAttack = true;
    public float swingCoolDown;


    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    public override void Attack()
    {
        if (canAttack)
        {
            animator.SetTrigger("swing");
            StartCoroutine(CoolDown());
        }
    }


    IEnumerator CoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(swingCoolDown);
        canAttack = true;
    }
}
