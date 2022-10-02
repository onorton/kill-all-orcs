using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{

    [SerializeField]
    private AudioClip _powerUpSound;

    private AudioManager _audioManager;

    public virtual void Use(GameObject target)
    {
        PlayPowerUpSound();
    }

    protected virtual void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayPowerUpSound()
    {
        _audioManager.Play(_powerUpSound);
    }
}
