using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletIconPrefab;

    private EventBus _eventBus;

    private Transform _bullets;

    private Image _weaponImage;

    private PlayerController _playerController;

    private const int UiSizePerUnit = 100;
    private const int UnitSize = 32;

    // Start is called before the first frame update
    private void Start()
    {
        _eventBus = FindObjectOfType<EventBus>();

        _eventBus.OnPlayerFiredWeapon += PlayerFiredWeapon;
        _eventBus.OnPlayerFinishedReloading += PlayerFinishedReloading;
        _eventBus.OnPlayerChangedWeapon += SwitchWeapon;

        _bullets = transform.Find("Bullets");

        _playerController = FindObjectOfType<PlayerController>();
        SwitchWeapon();
    }

    private void PlayerFinishedReloading(int numberOfBullets)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            Instantiate(_bulletIconPrefab, _bullets);
        }
    }

    private void SwitchWeapon()
    {
        // Clear out previous bullets
        for (int i = 0; i < _bullets.childCount; i++)
        {
            Destroy(_bullets.GetChild(i).gameObject);
        }

        var playerCurrentWeapon = FindObjectOfType<PlayerController>().GetCurrentWeapon();

        var weapon = playerCurrentWeapon.GetComponent<ProjectileSpawner>();
        _bulletIconPrefab.GetComponent<Image>().sprite = weapon.BulletIconImage;
        _bulletIconPrefab.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(weapon.BulletIconImage.rect.width / UnitSize * UiSizePerUnit, weapon.BulletIconImage.rect.height / UnitSize * UiSizePerUnit);

        PlayerFinishedReloading(weapon.CurrentNumberOfShots);
        _weaponImage = transform.Find("Weapon Icon").GetComponent<Image>();
        _weaponImage.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        _weaponImage.rectTransform.sizeDelta = new Vector2(_weaponImage.sprite.rect.width / UnitSize * UiSizePerUnit, _weaponImage.sprite.rect.height / UnitSize * UiSizePerUnit);
    }

    private void PlayerFiredWeapon()
    {
        Destroy(_bullets.GetChild(0).gameObject);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        _eventBus.OnPlayerFiredWeapon -= PlayerFiredWeapon;
        _eventBus.OnPlayerFinishedReloading -= PlayerFinishedReloading;
        _eventBus.OnPlayerChangedWeapon -= SwitchWeapon;
    }
}
