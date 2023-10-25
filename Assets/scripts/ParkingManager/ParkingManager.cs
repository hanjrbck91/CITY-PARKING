using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingManager : MonoBehaviour
{
    #region Fields/references
    GamePlayManager gameManager;

    public Transform car; // Reference to the current car
    public float winThreshold = 1.5f; // Adjust this value as needed
    public float checkDelay = 1.0f; // Delay in seconds

    private Transform parkingSlot; // Reference to the parking slot

    private ParkingSlotPositionSetter parkingSlotPositionSetter;
    #endregion


    #region Start/Update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GamePlayManager>();
        if (gameManager == null)
        {
            Debug.LogError("GamePlayManager not found in the scene.");
        }

        // Find the ParkingSlotPositionSetter script in the scene
        parkingSlotPositionSetter = FindObjectOfType<ParkingSlotPositionSetter>();
        if (parkingSlotPositionSetter == null)
        {
            Debug.LogError("ParkingSlotPositionSetter not found in the scene.");
        }
        else
        {
            // Set the parking slot position using the method from ParkingSlotPositionSetter
            parkingSlot = parkingSlotPositionSetter.parkingSlot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (car != null && Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(CheckWinWithDelay());
            Debug.Log("Button P is pressed");
        }
    }
    #endregion


    #region Methods
    // Set the current car
    public void SetCar(GameObject newCar)
    {
        car = newCar.transform;
    }

    // Check if the car is within the win threshold of the parking slot with a delay
    private IEnumerator CheckWinWithDelay()
    {
        yield return new WaitForSeconds(checkDelay);

        if (car != null && parkingSlot != null && Vector3.Distance(car.position, parkingSlot.position) <= winThreshold)
        {
            // Call the LevelComplete method in GamePlayManager to handle winning logic
            if (gameManager != null)
            {
                gameManager.HandleLevelComplete();
            }
            // Handle win condition
            Debug.Log("You've parked the car!");
        }
        else
        {
            // Handle not parked condition
            Debug.Log("Not parked yet. Try again!");
            Debug.Log(car.position.x - parkingSlot.position.x);
        }
    }
    #endregion
}
