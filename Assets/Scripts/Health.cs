using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _lives;

    private AudioManager _audioManager;

    [SerializeField]
    private AudioClip _hitSound;

    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = FindObjectOfType<EventBus>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var projectileComponent = col.gameObject.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            if ((gameObject.tag == "Player") != (projectileComponent.OwnerTag == "Player"))
            {
                DecreaseLife();
                Destroy(col.gameObject);
            }

        }


    }

    public void IncreaseLife()
    {
        Debug.Log("SUp?");
        _lives++;
        _eventBus.CharacterGainingLifeEvent(gameObject, _lives);
    }

    public void DecreaseLife()
    {
        _audioManager.Play(_hitSound);
        _lives -= 1;
        _eventBus.CharacterLosingLifeEvent(gameObject, _lives);

        if (_lives <= 0)
        {
            _eventBus.CharacterDyingEvent(gameObject);
            Destroy(gameObject);
        }
    }

}
