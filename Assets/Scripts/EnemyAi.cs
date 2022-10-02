using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{

    private Transform _target;

    // Don't let enemy move too close
    [SerializeField]
    private float _minimumDistanceFromTarget;

    private CharacterController _characterController;

    private ProjectileSpawner _projectileSpawner;

    // Initially inactive when spawning
    private float _timeUntilActiveSeconds = 1.0f;

    private void Start()
    {
        _target = GameObject.FindGameObjectsWithTag("Player").Single(x => x.name == "Player").transform;
        _characterController = GetComponent<CharacterController>();
        _projectileSpawner = GetComponentInChildren<ProjectileSpawner>();
    }

    private void Update()
    {
        _timeUntilActiveSeconds -= Time.deltaTime;
        if (_timeUntilActiveSeconds > 0.0f)
        {
            return;
        }

        var toTarget = _target.position - transform.position;



        // Retreat if too close
        if (Vector3.Distance(_target.position, transform.position) < _minimumDistanceFromTarget)
        {
            _characterController.Move(-toTarget.normalized);
        }
        else
        {
            if (HasLineOfSight())
            {
                _characterController.Move(toTarget.normalized);
            }
            else
            {
                // move perpendicular
                _characterController.Move(new Vector3(-toTarget.y, toTarget.x, 0).normalized);
            }
        }

        _projectileSpawner.transform.right = _target.position - _projectileSpawner.transform.position;

        // For now always try to shoot at player
        if (HasLineOfSight())
        {
            _characterController.FireGun(_target.position);
        }

    }

    private bool HasLineOfSight()
    {
        var toTarget = _target.position - transform.position;

        return !Physics2D.RaycastAll(transform.position, toTarget.normalized, toTarget.magnitude, LayerMask.GetMask("Blocks Vision")).Any();

    }
}
