using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAnimationUI : MonoBehaviour
{

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = FindObjectOfType<EventBus>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _eventBus.OnPlayerStartedReloading += PlayReloadAnimation;
        _eventBus.OnPlayerInterruptedReloading += EndOfAnimation;

        _eventBus.OnPlayerChangedWeapon += EndOfAnimation;
    }

    private void OnDestroy()
    {
        _eventBus.OnPlayerStartedReloading -= PlayReloadAnimation;
        _eventBus.OnPlayerInterruptedReloading -= EndOfAnimation;
        _eventBus.OnPlayerChangedWeapon -= EndOfAnimation;
    }

    private void PlayReloadAnimation(float timeToReload)
    {
        var normalAnimationTime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        _spriteRenderer.enabled = true;
        _animator.Play("Reload", 0, 0);
        _animator.speed = normalAnimationTime / timeToReload;

    }

    public void EndOfAnimation()
    {
        _spriteRenderer.enabled = false;
    }
}
