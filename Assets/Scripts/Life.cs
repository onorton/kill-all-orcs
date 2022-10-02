using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : PowerUp
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Use(GameObject target)
    {
        base.Use(target);
        var health = target.GetComponent<Health>();
        if (health != null)
        {
            health.IncreaseLife();
        }
    }
}
