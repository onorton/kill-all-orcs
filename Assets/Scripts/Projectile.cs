using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 InitialDirection { get; set; }

    public float Speed = 5.0f;

    public string OwnerTag;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(InitialDirection * Speed * Time.deltaTime);
    }
}
