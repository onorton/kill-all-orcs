using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    private Vector2 _mousePosition;

    private CharacterController _characterController;

    [SerializeField]
    private Transform aimingCursor;

    private ProjectileSpawner _projectileSpawner;

    private Transform _weapons;

    // Of available weapons, which one is the player using?
    private int _weaponIndex = 0;

    private EventBus _eventBus;

    private int _numberOfWeapons => _weapons.childCount;


    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _projectileSpawner = transform.GetComponentInChildren<ProjectileSpawner>();

        _weapons = transform.Find("Weapons");

        _eventBus = FindObjectOfType<EventBus>();

        _playerInputActions.Player.SwitchWeapon.performed += WeaponSwitched;
        _playerInputActions.Player.Reload.performed += ForceReload;
    }

    private void WeaponSwitched(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            var value = obj.ReadValue<float>();

            if (_numberOfWeapons == 1)
            {
                return;
            }

            if (value > 0)
            {
                _weaponIndex = (_weaponIndex + 1) % _numberOfWeapons;
            }
            else
            {
                _weaponIndex = (_weaponIndex + 1) % _numberOfWeapons;
            }
            SetWeapon();
        }
    }

    private void ForceReload(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            GetCurrentWeapon().GetComponent<ProjectileSpawner>().Reload();
        }
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var movementDirection = _playerInputActions.Player.Movement.ReadValue<Vector2>();

        _weapons.transform.right = aimingCursor.position - _weapons.transform.position;

        // Allow player to hold button like AI effectively do
        if (_playerInputActions.Player.FireGun.IsPressed())
        {
            FireGun();
        }

        _characterController.Move(movementDirection);
    }

    private void FireGun()
    {
        GetCurrentWeapon().GetComponent<ProjectileSpawner>().SpawnProjectile(aimingCursor.position);
    }

    public GameObject GetCurrentWeapon()
    {
        return _weapons.GetChild(_weaponIndex).gameObject;
    }

    public void PickupWeapon(GameObject weaponPrefab)
    {
        var weapon = Instantiate(weaponPrefab, _weapons);
        Destroy(weapon.GetComponent<Pickupable>());
        Destroy(weapon.GetComponent<PowerUp>());
        Destroy(weapon.GetComponent<Collider2D>());
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localPosition = Vector3.zero;
        weapon.layer = LayerMask.NameToLayer("Default");
        // To avoid shooting oneself
        weapon.tag = "Player";
        _weaponIndex = _numberOfWeapons - 1;
        SetWeapon();
    }

    private void SetWeapon()
    {
        for (var i = 0; i < _numberOfWeapons; i++)
        {
            if (i == _weaponIndex)
            {
                _weapons.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _weapons.GetChild(i).GetComponent<ProjectileSpawner>().InterruptReload();
                _weapons.GetChild(i).GetComponent<ProjectileSpawner>().ResetReloading();
                _weapons.GetChild(i).gameObject.SetActive(false);
            }
        }
        _eventBus.PlayerChangedWeaponEvent();

    }


}
