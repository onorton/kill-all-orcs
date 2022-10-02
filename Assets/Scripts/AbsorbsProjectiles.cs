using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbsProjectiles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var projectileComponent = col.gameObject.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            Destroy(col.gameObject);
        }
    }

}