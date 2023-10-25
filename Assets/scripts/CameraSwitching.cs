using UnityEngine;
using Cinemachine;

public class CameraSwitching : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;
    private int currentCameraIndex = 0;

    private void Start()
    {
        // Set the first camera active by default
        SwitchCamera(0);
    }

    private void Update()
    {
        // Use number keys to switch between cameras (1, 2, 3, ...)
        for (int i = 1; i <= cameras.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                SwitchCamera(i - 1);
                break;
            }
        }
    }

    private void SwitchCamera(int cameraIndex)
    {
        // Deactivate all cameras
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // Activate the selected camera
        cameras[cameraIndex].gameObject.SetActive(true);
        currentCameraIndex = cameraIndex;
    }
}
