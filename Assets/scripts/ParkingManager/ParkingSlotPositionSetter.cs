using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSlotPositionSetter : MonoBehaviour
{
    public Transform parkingSlot; // Reference to the parking slot

    // Method to set the parking slot position
    public void SetParkingSlotPosition(Transform newParkingSlot)
    {
        parkingSlot = newParkingSlot;
    }
}
