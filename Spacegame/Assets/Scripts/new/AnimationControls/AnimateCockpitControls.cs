using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCockpitControls : MonoBehaviour
{
    [SerializeField] Transform joystick;

    [SerializeField] Vector3 joystickRange = Vector3.zero;

    [SerializeField] List<Transform> throttles;

    [SerializeField] float throttleRange = 35;

    [SerializeField] ShipMovementInput movementInput;

    IMovementControls ControlInput => movementInput.MovementControls;

    void Update()
    {
        joystick.localRotation = Quaternion.Euler(
            ControlInput.PitchAmount * joystickRange.x,
            ControlInput.YawAmount * joystickRange.y,
            ControlInput.RollAmount * joystickRange.z
        );

        Vector3 throttleRotation = throttles[0].localRotation.eulerAngles;
        throttleRotation.x = ControlInput.ThrustAmount * throttleRange;
        foreach(Transform throttle in throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }
    }
}
