using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] ShipMovementInput movementInput;

    [SerializeField]
    [Range(1000f, 10000f)]
    float thrustForce = 7500f,
          pitchForce = 6000f,
          rollForce = 1000f,
          yawForce = 2000f;
    Rigidbody rigidBody;
    [SerializeField]
    [Range(-1f, 1f)]
    float thrustAmount = 0f,
          pitchAmount = 0f,
          rollAmount = 0f,
          yawAmount = 0f;

    IMovementControls controlInput => movementInput.MovementControls;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        thrustAmount = controlInput.ThrustAmount;
        rollAmount = controlInput.RollAmount;
        yawAmount = controlInput.YawAmount;
        pitchAmount = controlInput.PitchAmount;
    }

    void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, pitchAmount)) 
        {
            rigidBody.AddTorque(transform.right * (pitchForce * pitchAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, rollAmount))
        {
            rigidBody.AddTorque(transform.forward * (rollForce * rollAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, yawAmount))
        {
            rigidBody.AddTorque(transform.up * (yawForce * yawAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, thrustAmount))
        {
            rigidBody.AddForce(transform.forward * (thrustForce * thrustAmount * Time.fixedDeltaTime));
        }
    }
}
