using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    private TextMeshProUGUI _wavesNumberText;
    private TextMeshProUGUI _enemiesKilledNumberText;

    private EventBus _eventBus;

    // Start is called before the first frame update
    private void Start()
    {
        _wavesNumberText = transform.Find("Panel/Waves/Number").GetComponent<TextMeshProUGUI>();
        _enemiesKilledNumberText = transform.Find("Panel/Enemies/Number").GetComponent<TextMeshProUGUI>();


        _eventBus = FindObjectOfType<EventBus>();
        _eventBus.OnPlayerKill += EnemyKilled;
        _eventBus.OnWaveReached += WaveReached;

    }

    private void EnemyKilled(int totalKills)
    {
        _enemiesKilledNumberText.text = $"{totalKills}";
    }

    private void WaveReached(int waveNumber)
    {
        _wavesNumberText.text = $"{waveNumber}";
    }
}
