using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCockpitControls : MonoBehaviour
{
    [SerializeField] Transform joystick;

    [SerializeField] Vector3 joystickRange = Vector3.zero;

    [SerializeField] List<Transform> throttles;

    [SerializeField] float throttleRange = 35;

    IMovementControls movementInput;
    void Update()
    {
        if (movementInput == null) return;

        joystick.localRotation = Quaternion.Euler(
            movementInput.PitchAmount * joystickRange.x,
            movementInput.YawAmount * joystickRange.y,
            movementInput.RollAmount * joystickRange.z
        );

        Vector3 throttleRotation = throttles[0].localRotation.eulerAngles;
        throttleRotation.x = movementInput.ThrustAmount * throttleRange;
        foreach(Transform throttle in throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }
    }

    public void Init(IMovementControls MovementControls)
    {
        movementInput = MovementControls;
    }
}
