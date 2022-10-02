using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingCursor : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    private Vector2 _previousMousePosition;

    private float _cursorSpeed = 10;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _previousMousePosition = _playerInputActions.Player.Aim.ReadValue<Vector2>();
    }

    // Update is called once per frame
    private void Update()
    {
        var mouseAimPosition = _playerInputActions.Player.Aim.ReadValue<Vector2>();
        var stickAimDelta = _playerInputActions.Player.StickAim.ReadValue<Vector2>();

        var newPosition = transform.position;
        if (stickAimDelta.magnitude > 0.05)
        {
            // Need to also adjust for camera
            newPosition = transform.position + new Vector3(stickAimDelta.x, stickAimDelta.y, 0) * _cursorSpeed * Time.deltaTime;
        }
        else if (mouseAimPosition != _previousMousePosition)
        {
            newPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseAimPosition.x, mouseAimPosition.y, 0));
            newPosition = new Vector3(newPosition.x, newPosition.y, 0);
        }

        newPosition.x = Mathf.Clamp(newPosition.x, Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x);
        newPosition.y = Mathf.Clamp(newPosition.y, Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y);

        transform.position = newPosition;

        _previousMousePosition = mouseAimPosition;

    }
}
