using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    public Sprite BulletIconImage;



    [SerializeField]
    private float _minimumTimeBetweenShotsSeconds;

    private float _timeUntilNextShotSeconds;


    [SerializeField]
    private float _reloadTimeSeconds;

    private float _timeUntilReloadCompletedSeconds;
    private bool _reloadCompleted = true;


    public int MaxNumberOfShots;

    public int CurrentNumberOfShots { get; set; }

    private Transform _spawnPoint;


    [SerializeField]
    private AudioClip _shotSound;

    private AudioManager _audioManager;


    private EventBus _eventBus;

    private bool _isPlayer => gameObject.tag == "Player";

    private void Awake()
    {
        CurrentNumberOfShots = MaxNumberOfShots;
    }

    private void Start()
    {
        _spawnPoint = transform.Find("Spawn Point");
        _audioManager = FindObjectOfType<AudioManager>();
        _eventBus = FindObjectOfType<EventBus>();
    }

    public void SpawnProjectile(Vector3 target)
    {
        InterruptReload();

        // Can't fire yet
        if (_timeUntilNextShotSeconds > 0.0f || !_reloadCompleted)
        {
            return;
        }

        var projectile = Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().OwnerTag = gameObject.tag;
        projectile.transform.right = (target - projectile.transform.position).normalized;
        projectile.GetComponent<Projectile>().InitialDirection = Vector3.right;
        _audioManager.Play(_shotSound);

        CurrentNumberOfShots -= 1;

        if (_isPlayer)
        {
            _eventBus.PlayerFiredWeaponEvent();
        }


        if (CurrentNumberOfShots == 0)
        {
            Reload();

        }
        else
        {
            // Reset timer
            _timeUntilNextShotSeconds = _minimumTimeBetweenShotsSeconds;
        }
    }

    public void Reload()
    {
        if (CurrentNumberOfShots < MaxNumberOfShots)
        {
            _reloadCompleted = false;
            _timeUntilReloadCompletedSeconds = _reloadTimeSeconds;
            if (_isPlayer)
            {
                _eventBus.PlayerReloadingEvent(_reloadTimeSeconds);
            }
        }
    }

    public void InterruptReload()
    {
        // If in the middle of reloading but can still fire, stop reloading
        if (!_reloadCompleted && CurrentNumberOfShots > 0)
        {
            if (_isPlayer)
            {
                _eventBus.PlayerInterruptedReloadingEvent();
            }
            _reloadCompleted = true;
        }
    }

    private void Update()
    {
        if (_isPlayer && _timeUntilReloadCompletedSeconds == _reloadTimeSeconds)
        {
            _eventBus.PlayerReloadingEvent(_reloadTimeSeconds);
        }

        _timeUntilNextShotSeconds = Mathf.Max(_timeUntilNextShotSeconds - Time.deltaTime, 0);
        _timeUntilReloadCompletedSeconds = Mathf.Max(_timeUntilReloadCompletedSeconds - Time.deltaTime, 0);
        if (_timeUntilReloadCompletedSeconds == 0 && !_reloadCompleted)
        {
            _reloadCompleted = true;
            if (_isPlayer)
            {
                _eventBus.PlayerFinishedReloadingEvent(MaxNumberOfShots - CurrentNumberOfShots);
            }
            CurrentNumberOfShots = MaxNumberOfShots;
            _timeUntilNextShotSeconds = 0;
        }
    }

    public void ResetReloading()
    {
        if (!_reloadCompleted)
        {
            _timeUntilReloadCompletedSeconds = _reloadTimeSeconds;
        }
    }
}
