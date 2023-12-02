using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera virtualCamera;
    public Camera regularCamera;

    private void Start()
    {
        // Ensure only one camera is enabled at the start
        EnableCamera(virtualCamera);
        DisableCamera(regularCamera);
    }

    private void Update()
    {
        // Check for input to switch cameras
        if (Input.GetButtonDown("CameraSwitch"))
        {
            SwitchCameras();
        }
    }

    private void SwitchCameras()
    {
        // Toggle camera states
        if (virtualCamera.enabled)
        {
            EnableCamera(regularCamera);
            DisableCamera(virtualCamera);
        }
        else
        {
            EnableCamera(virtualCamera);
            DisableCamera(regularCamera);
        }
    }

    private void EnableCamera(Camera camera)
    {
        if (camera != null)
        {
            camera.enabled = true;
        }
    }

    private void DisableCamera(Camera camera)
    {
        if (camera != null)
        {
            camera.enabled = false;
        }
    }
}
