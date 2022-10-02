using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : PowerUp
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Use(GameObject target)
    {
        base.Use(target);
        target.GetComponent<PlayerController>().PickupWeapon(gameObject);

    }
}
