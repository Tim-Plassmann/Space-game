using UnityEngine;

public class SmoothPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    public float pitchDamping = 5f;
    public float yawDamping = 5f;
    public float speedDamping = 2f;

    private float currentPitchSpeed;
    private float currentYawSpeed;
    private float currentForwardSpeed;

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float rollInput = Input.GetAxis("Horizontal");

        // Update forward movement based on input
        currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, verticalInput * moveSpeed, Time.deltaTime * speedDamping);
        Vector3 moveAmount = transform.forward * currentForwardSpeed * Time.deltaTime;
        transform.Translate(moveAmount, Space.World);

        // Roll the player based on input
        float rollAmount = rollInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, -rollAmount);
    }

    private void RotatePlayer()
    {
        float pitchInput = Input.GetAxis("Pitch");
        float yawInput = Input.GetAxis("Yaw");

        // Update pitch and yaw speed with damping
        currentPitchSpeed = Mathf.Lerp(currentPitchSpeed, pitchInput * rotationSpeed, Time.deltaTime * pitchDamping);
        currentYawSpeed = Mathf.Lerp(currentYawSpeed, yawInput * rotationSpeed, Time.deltaTime * yawDamping);

        // Apply rotation without affecting forward movement
        transform.localRotation *= Quaternion.Euler(Vector3.left * currentPitchSpeed * Time.deltaTime);
        transform.rotation *= Quaternion.Euler(Vector3.up * currentYawSpeed * Time.deltaTime);
    }
}
