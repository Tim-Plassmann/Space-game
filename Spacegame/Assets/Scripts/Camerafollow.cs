using UnityEngine;

public class FollowShip : MonoBehaviour
{
    public Transform target; // The ship's transform
    public Vector3 positionOffset = new Vector3(0f, 2f, 8f); // Offset from the ship's position
    public Vector3 rotationOffset = new Vector3(20f, 0f, 0f); // Offset for camera rotation

    void Update()
    {
        if (target != null)
        {
            // Update the camera position based on the ship's position and offset
            transform.position = target.position + positionOffset;

            // Apply an additional rotation offset to the camera
            transform.rotation = Quaternion.Euler(rotationOffset);

            // Rotate the camera to follow the ship's rotation
            transform.Rotate(target.eulerAngles);
        }
    }
}
