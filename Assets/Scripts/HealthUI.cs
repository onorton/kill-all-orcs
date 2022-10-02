using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    [SerializeField]
    private GameObject _lifeImage;

    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = FindObjectOfType<EventBus>();

        _eventBus.OnLosingLife += LivesChanged;
        _eventBus.OnGainingLife += LivesChanged;
    }

    private void LivesChanged(GameObject character, int livesRemaining)
    {
        if (character.tag != "Player")
        {
            return;
        }

        for (var i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (var i = 0; i < livesRemaining; i++)
        {
            Instantiate(_lifeImage, transform);
        }

    }

    private void OnDestroy()
    {
        _eventBus.OnLosingLife -= LivesChanged;
        _eventBus.OnGainingLife -= LivesChanged;

    }

}
