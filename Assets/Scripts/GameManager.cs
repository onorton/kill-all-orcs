using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _playerKills;

    private EventBus _eventBus;

    [SerializeField]
    private GameObject _gameOverPanel;


    private void Start()
    {
        Time.timeScale = 0;
        _playerKills = 0;

        _eventBus = GetComponent<EventBus>();
        _eventBus.OnDying += CharacterDied;
    }

    public void CharacterDied(GameObject character)
    {
        if (character.tag == "Player")
        {
            Time.timeScale = 0;
            _gameOverPanel.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            _playerKills += 1;
            _eventBus.PlayerKillEvent(_playerKills);
        }
    }

    public void OnDestroy()
    {
        _eventBus.OnDying -= CharacterDied;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
    }



}
