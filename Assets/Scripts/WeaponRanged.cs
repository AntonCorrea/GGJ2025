using System.Collections;
using UnityEngine;

public class WeaponRanged : WeaponBase
{

    bool canAttack = true;

    public float coolDown;

    public GameObject bullet;

    public Transform bulletSpawn;

    // Start is called before the first frame update
    public override void Attack()
    {
        if (canAttack)
        {
            Shoot();
            StartCoroutine(CoolDown());
        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(bullet);
        b.transform.position = bulletSpawn.position;
        b.transform.rotation = bulletSpawn.rotation;
    }

    IEnumerator CoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }
}
