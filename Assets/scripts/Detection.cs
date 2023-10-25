using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    GamePlayManager gameManager;

    // Add a method to initialize the gameManager reference
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GamePlayManager").GetComponent<GamePlayManager>();
    }

    #region OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "ParkingPoint")
        {
            gameManager.HandleLevelComplete();
        }

        if (other.gameObject.tag == "TrafficCar")
        {
            gameManager.HandleLevelFailed();
        }
    }

    #endregion
}
