using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDisplay : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Adjust the rotation speed as needed.

    // Update is called once per frame
    void Update()
    {
        // Rotate the game object around the Y-axis over time.
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
