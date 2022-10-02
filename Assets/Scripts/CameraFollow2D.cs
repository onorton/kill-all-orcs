using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    private Transform _target;
    private Vector3 _previousPosition;

    private Camera _camera;
    private Vector3 _velocity = Vector3.zero;


    // In viewport space
    private const float padding = 0.3f;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _target = GameObject.FindGameObjectsWithTag("Player").Single(x => x.name == "Player").transform;
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        var point = _camera.WorldToViewportPoint(_target.position);

        var deltaX = 0f;
        var deltaY = 0f;

        if (point.x > 1.0f - padding)
        {
            deltaX = point.x - (1 - padding);

        }
        else if (point.x < padding)
        {
            deltaX = point.x - padding;
        }

        if (point.y > 1.0f - padding)
        {
            deltaY = point.y - (1 - padding);

        }
        else if (point.y < padding)
        {
            deltaY = point.y - padding;
        }


        var destination = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f) + new Vector3(deltaX, deltaY, 0f));

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, 0.3f);
    }
}
