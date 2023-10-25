using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelFriction : MonoBehaviour
{
    public PhysicMaterial wheelPhysicMaterial; // Assign the "CarWheelPhysicMaterial" from the Inspector.

    private WheelCollider[] wheelColliders;

    private void Start()
    {
        wheelColliders = GetComponentsInChildren<WheelCollider>();
        ApplyFrictionToWheels();
    }

    private void ApplyFrictionToWheels()
    {
        foreach (WheelCollider wheelCollider in wheelColliders)
        {
            if (wheelPhysicMaterial != null)
            {
                // Set the friction properties for the wheel collider using the Physic Material.
                WheelFrictionCurve sidewaysFriction = wheelCollider.sidewaysFriction;
                sidewaysFriction.extremumValue = 3f;
                sidewaysFriction.extremumSlip = 0.2f;
                sidewaysFriction.asymptoteValue =3f;
                sidewaysFriction.asymptoteSlip = 0.5f;
                sidewaysFriction.stiffness = 2f;
                wheelCollider.sidewaysFriction = sidewaysFriction;

                WheelFrictionCurve forwardFriction = wheelCollider.forwardFriction;
                forwardFriction.extremumValue = wheelPhysicMaterial.dynamicFriction;
                forwardFriction.extremumSlip = 1f;
                forwardFriction.asymptoteValue = wheelPhysicMaterial.staticFriction;
                forwardFriction.asymptoteSlip = 2f;
                forwardFriction.stiffness = wheelPhysicMaterial.frictionCombine == PhysicMaterialCombine.Minimum ? 0f : 1f;
                wheelCollider.forwardFriction = forwardFriction;
            }
        }
    }
}
