using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCockpitControls : MonoBehaviour
{
    [SerializeField] Transform joystick;

    [SerializeField] Vector3 joystickRange = Vector3.zero;

    [SerializeField] List<Transform> throttles;

    [SerializeField] float throttleRange = 35;

    IMovementControls movementControls;
    void Update()
    {
        if (movementControls == null) return;

        joystick.localRotation = Quaternion.Euler(
            movementControls.PitchAmount * joystickRange.x,
            movementControls.YawAmount * joystickRange.y,
            movementControls.RollAmount * joystickRange.z
        );

        Vector3 throttleRotation = throttles[0].localRotation.eulerAngles;
        throttleRotation.x = movementControls.ThrustAmount * throttleRange;
        foreach(Transform throttle in throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }
    }

    public void Init(IMovementControls MovementControls)
    {
        movementControls = MovementControls;
    }
}
