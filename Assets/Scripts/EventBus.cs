using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{

    public delegate void CharacterDiedEventHandler(GameObject gameObject);
    public event CharacterDiedEventHandler OnDying;


    public delegate void CharacterLifeChangeEventHandler(GameObject gameObject, int livesRemaining);
    public event CharacterLifeChangeEventHandler OnLosingLife;
    public event CharacterLifeChangeEventHandler OnGainingLife;


    public delegate void WaveReachedEventHandler(int waveNumber);
    public event WaveReachedEventHandler OnWaveReached;

    public delegate void PlayerKillEventHandler(int totalKills);
    public event PlayerKillEventHandler OnPlayerKill;


    public delegate void PlayerReloadingEventHandler(float timeToReload);
    public event PlayerReloadingEventHandler OnPlayerStartedReloading;

    public delegate void PlayerFinishedReloadingEventHandler(int numberOfBullets);
    public event PlayerFinishedReloadingEventHandler OnPlayerFinishedReloading;

    public delegate void PlayerInterrupedReloadingEventHandler();
    public event PlayerInterrupedReloadingEventHandler OnPlayerInterruptedReloading;



    public delegate void PlayerFiredWeaponEventHandler();
    public event PlayerFiredWeaponEventHandler OnPlayerFiredWeapon;

    public delegate void PlayerChangedWeaponEventHandler();
    public event PlayerChangedWeaponEventHandler OnPlayerChangedWeapon;



    public void CharacterDyingEvent(GameObject character)
    {
        OnDying.Invoke(character);
    }

    public void CharacterLosingLifeEvent(GameObject character, int livesRemaining)
    {
        OnLosingLife.Invoke(character, livesRemaining);
    }

    public void CharacterGainingLifeEvent(GameObject character, int livesRemaining)
    {
        OnGainingLife.Invoke(character, livesRemaining);
    }


    public void WaveReachedEvent(int waveNumber)
    {
        OnWaveReached.Invoke(waveNumber);
    }

    public void PlayerKillEvent(int totalKills)
    {
        OnPlayerKill.Invoke(totalKills);
    }

    public void PlayerReloadingEvent(float timeToReload)
    {
        OnPlayerStartedReloading.Invoke(timeToReload);
    }

    public void PlayerFinishedReloadingEvent(int numberOfBullets)
    {
        OnPlayerFinishedReloading.Invoke(numberOfBullets);
    }

    public void PlayerInterruptedReloadingEvent()
    {
        OnPlayerInterruptedReloading.Invoke();
    }


    public void PlayerFiredWeaponEvent()
    {
        OnPlayerFiredWeapon.Invoke();
    }

    public void PlayerChangedWeaponEvent()
    {
        OnPlayerChangedWeapon.Invoke();
    }
}
