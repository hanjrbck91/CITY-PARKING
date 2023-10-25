using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Rigidbody playerRB;
    public Vector3 Offset;
    public float speed;

    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
        cameraTransform = transform;
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate()
    {
        Vector3 playerForward = (playerRB.velocity + playerRB.transform.forward).normalized;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position,
            player.position + playerRB.transform.TransformVector(Offset) + playerForward * (-5f),
            speed * Time.fixedDeltaTime);
        cameraTransform.LookAt(player);
    }
}