using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    private float _minimumDistanceFromPlayer = 5;
    private float _maximumDistanceFromPlayer = 8;


    [SerializeField]
    private GameObject[] _enemyPrefabs;


    [SerializeField]
    private Bounds _bounds;

    // Don't spawn in walls
    private List<Bounds> _walls;

    private int numberOfEnemiesToSpawn = 1;

    private int _spawnIntervalSeconds = 10;

    private float _timePassed;

    private int _waveNumber;

    private EventBus _eventBus;

    private void Start()
    {
        _walls = new List<Bounds>();
        var wallsObject = GameObject.Find("Walls");

        for (var i = 0; i < wallsObject.transform.childCount; i++)
        {
            var wall = wallsObject.transform.GetChild(i);
            Debug.Log(wall.GetComponent<SpriteRenderer>().size.x);
            _walls.Add(new Bounds(wall.position, new Vector3(wall.GetComponent<SpriteRenderer>().size.x + 0.5f, wall.GetComponent<SpriteRenderer>().size.y + 0.5f, 0)));
        }

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Spawn();
        }
        numberOfEnemiesToSpawn += 1;
        _waveNumber = 1;

        _eventBus = FindObjectOfType<EventBus>();
        _eventBus.WaveReachedEvent(_waveNumber);
    }

    private void Update()
    {

        var newTimePassed = _timePassed + Time.deltaTime;
        if (Mathf.FloorToInt(newTimePassed) > Mathf.FloorToInt(_timePassed) && Mathf.FloorToInt(newTimePassed) % _spawnIntervalSeconds == 0)
        {
            for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                Spawn();
            }

            // % chance of increasing?
            numberOfEnemiesToSpawn += 1;
            _waveNumber++;
            _eventBus.WaveReachedEvent(_waveNumber);
        }

        _timePassed = newTimePassed;
    }

    private void Spawn()
    {
        var spawnPositionFound = false;
        while (!spawnPositionFound)
        {
            var spawnPositionFromPlayerX = Random.Range(-_maximumDistanceFromPlayer, _maximumDistanceFromPlayer);
            var spawnPositionFromPlayerY = Random.Range(-_maximumDistanceFromPlayer, _maximumDistanceFromPlayer);
            var spawnPosition = _player.position + new Vector3(spawnPositionFromPlayerX, spawnPositionFromPlayerY);
            if (Vector3.Distance(spawnPosition, _player.position) <= _maximumDistanceFromPlayer && Vector3.Distance(spawnPosition, _player.position) >= _minimumDistanceFromPlayer && _bounds.Contains(spawnPosition) && _walls.All(w => !w.Contains(spawnPosition)))
            {
                spawnPositionFound = true;
                Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], spawnPosition, Quaternion.identity);
            }
        }
    }

}
