using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var playerController = col.gameObject.GetComponent<PlayerController>();
        if (playerController != null && gameObject.GetComponent<PowerUp>() != null)
        {
            var player = col.gameObject;
            var powerUp = gameObject.GetComponent<PowerUp>();

            powerUp.Use(player);

            Destroy(gameObject);
        }
    }
}
