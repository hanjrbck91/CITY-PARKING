using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarJ_Controller : MonoBehaviour
{

    public ParkingManager parkingManager; // Reference to the ParkingManager script

    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    public Transform centerOfMass; // Center of Mass object

    // Inputs of Motion
    public float gasInput;
    public float brakeInput;
    public float steeringInput;

    public float motorPower;
    public float brakePower;
    public float slipAngle;

    public float speedLimit = 30f; // The maximum speed for this car, set it individually in the Unity Inspector

    private Rigidbody playerRB;
    private float speed;
    // Maximum steering angle of the wheels
    public float maxSteeringAngle = 30f;


    public float driftForce = 500f;

    // Maximum height difference threshold to prevent flipping
    public float maxHeightDifference = 0.5f;

    public Text speedometerText; // Reference to the UI Text element
    private float currentDisplayedSpeedKMH = 0f; // The currently displayed speed value


    // Start is called before the first frame update
    void Start()
    {

        playerRB = GetComponent<Rigidbody>();

        // Set the center of mass of the car
        if (centerOfMass != null)
        {
            playerRB.centerOfMass = centerOfMass.localPosition;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check for 'space' key press to stop the car
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandBrake();
            Debug.Log("button Space is Pressed ");
        }

        // Update the speedometer text
        UpdateSpeedometerText();

        // Limit the car's speed based on the speed limit
        LimitSpeed();


        speed = playerRB.velocity.magnitude;
        MoveWheels();
        CheckInput();
        ApplyMotor();
        ApplySteering();
        ApplyBrake();

        // Call PreventFlipping() method to handle flipping prevention
        PreventFlipping();
    }
    void UpdateSpeedometerText()
    {
        // Calculate the current speed of the car in km/h
        float currentSpeedKMH = playerRB.velocity.magnitude * 3.6f; // Convert m/s to km/h

        // Smoothly update the displayed speed value towards the actual speed value
        currentDisplayedSpeedKMH = Mathf.Lerp(currentDisplayedSpeedKMH, currentSpeedKMH, Time.deltaTime * 5f);

        // Update the speedometer text
        if (speedometerText != null)
        {
            speedometerText.text =currentSpeedKMH.ToString("F0");
        }
    }

    void LimitSpeed()
    {
        // Calculate the current speed of the car
        float currentSpeed = playerRB.velocity.magnitude;

        // If the current speed exceeds the speed limit, reduce the car's velocity to match the speed limit
        if (currentSpeed > speedLimit)
        {
            // Calculate the velocity reduction factor to match the speed limit
            float velocityReductionFactor = speedLimit / currentSpeed;

            // Apply the reduction factor to the car's velocity to limit the speed
            playerRB.velocity *= velocityReductionFactor;
        }
    }

    // Function to stop the car
    private void HandBrake()
    {
        // Apply strong braking force to all wheels with a power multiplier for the handbrake
        ApplyBrake();

        // Reset the steering angle to induce drifting
        float handBrakeSteeringAngle = steeringInput * 0.5f; // Adjust the steering angle for drifting
        colliders.FLWheel.steerAngle = handBrakeSteeringAngle;
        colliders.FRWheel.steerAngle = handBrakeSteeringAngle;

        // Apply a force to induce drifting while the handbrake is engaged
        Vector3 relativeVelocity = transform.InverseTransformDirection(playerRB.velocity);
        float driftForceFactor = Mathf.Clamp01(relativeVelocity.z / 5f); // Adjust the divisor as needed
        Vector3 driftForceVector = -transform.forward * driftForce * driftForceFactor;
        playerRB.AddForce(driftForceVector);

        // If the car is almost stationary, reset the velocities to bring it to a complete stop
        if (playerRB.velocity.sqrMagnitude < 0.1f)
        {
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
        }
    }


    void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);

        // Fixed code to brake even after going in reverse
        float movingDirection = Vector3.Dot(transform.forward, playerRB.velocity);
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }
    }

    void ApplyBrake()
    {
        // Value difference is because front wheels have more braking force than the back wheels in a realistic car
        colliders.FLWheel.brakeTorque = brakeInput * brakePower * brakePower * 1700f;
        colliders.FRWheel.brakeTorque = brakeInput * brakePower * brakePower * 1700f;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower * brakePower * 1300f;
        colliders.RRWheel.brakeTorque = brakeInput * brakePower * brakePower * 1300f;
    }

    void ApplyMotor()
    {
        colliders.RRWheel.motorTorque = motorPower * gasInput;
        colliders.RLWheel.motorTorque = motorPower * gasInput;
    }

    void ApplySteering()
    {
        float steeringAngle = maxSteeringAngle * steeringInput;

        // Reduce steering angle based on the car's speed (optional, you can remove this if you don't want it)
        float speedFactor = Mathf.Lerp(1f, 0.3f, speed / 10f);
        steeringAngle *= speedFactor;

        // Adjust steering angle based on the car's slip angle (optional, you can remove this if you don't want it)
        float slipAngleFactor = Mathf.Lerp(1f, 0.3f, slipAngle / 45f);
        steeringAngle *= slipAngleFactor;

        // Apply the calculated steering angle to the front wheels
        colliders.FLWheel.steerAngle = steeringAngle;
        colliders.FRWheel.steerAngle = steeringAngle;
    }

    void MoveWheels()
    {
        UpdateWheel(colliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(colliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(colliders.RRWheel, wheelMeshes.RRWheel);
        UpdateWheel(colliders.RLWheel, wheelMeshes.RLWheel);
    }

    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);

        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }

    void PreventFlipping()
    {
        // Calculate the height difference between left and right wheels
        float leftWheelHeight = colliders.FLWheel.transform.position.y;
        float rightWheelHeight = colliders.FRWheel.transform.position.y;
        float heightDifference = Mathf.Abs(leftWheelHeight - rightWheelHeight);

        // If the height difference exceeds the threshold, apply opposite force to prevent flipping
        if (heightDifference > maxHeightDifference)
        {
            float force = heightDifference * playerRB.mass * 10f; // Adjust the force multiplier as needed
            playerRB.AddForceAtPosition(Vector3.up * force, centerOfMass.position);
        }
    }
}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider RRWheel;
    public WheelCollider RLWheel;
}

[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer RRWheel;
    public MeshRenderer RLWheel;
}
