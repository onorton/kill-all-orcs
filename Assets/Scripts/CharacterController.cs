using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    private ProjectileSpawner _projectileSpawner;

    private void Start()
    {
        _projectileSpawner = transform.GetComponentInChildren<ProjectileSpawner>();
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * Time.deltaTime * _speed);
    }

    public void FireGun(Vector3 target)
    {
        _projectileSpawner.SpawnProjectile(target);

    }
}
