using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    [SerializeField]
    private Transform _player;

    private float _minimumDistanceFromPlayer = 5;
    private float _maximumDistanceFromPlayer = 8;

    // Don't spawn in walls
    private List<Bounds> _walls;


    [SerializeField]
    private List<GameObject> _powerUpPrefabs;

    [SerializeField]
    private List<GameObject> _weaponPrefabs;


    private int _spawnIntervalSeconds = 10;

    private float _timePassed;

    [SerializeField]
    private Bounds _bounds;


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

    }

    private void Update()
    {

        var newTimePassed = _timePassed + Time.deltaTime;
        if (Mathf.FloorToInt(newTimePassed) > Mathf.FloorToInt(_timePassed) && Mathf.FloorToInt(newTimePassed) % _spawnIntervalSeconds == 0)
        {
            Spawn();
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

                if (Random.Range(0.0f, 1.0f) < 0.4f)
                {
                    var weaponIndex = Random.Range(0, _weaponPrefabs.Count);
                    Instantiate(_weaponPrefabs[weaponIndex], spawnPosition, Quaternion.identity);
                    // Only once;
                    _weaponPrefabs.RemoveAt(weaponIndex);
                }
                else
                {
                    Instantiate(_powerUpPrefabs[Random.Range(0, _powerUpPrefabs.Count)], spawnPosition, Quaternion.identity);
                }
            }
        }
    }


}
