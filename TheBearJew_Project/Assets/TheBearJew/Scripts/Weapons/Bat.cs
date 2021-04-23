using System.Collections;
using UnityEngine;

public class Bat : Weapon
{
    public override void Shoot()
    {
        var animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        animator.SetTrigger("BatSlash");
    }
}
