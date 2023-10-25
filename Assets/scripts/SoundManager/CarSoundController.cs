using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    [SerializeField] private string carType; // Define the car type (e.g., "Sedan", "SUV", etc.)

    #region OnEnabel/OnDisable
    private void OnEnable()
    {
        // Subscribe to car engine sound events when the object is enabled
        SoundManager.OnCarSound += PlayCarEngineSound;
    }

    private void OnDisable()
    {
        // Unsubscribe from car engine sound events when the object is disabled
        SoundManager.OnCarSound -= PlayCarEngineSound;
    }
    #endregion
    private void PlayCarEngineSound(string carType)
    {
        // Implement code to play the car engine sound for the specified car type
        if (carType == this.carType)
        {
            // Play the engine sound for this car
        }
    }
}
