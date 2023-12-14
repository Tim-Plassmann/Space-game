using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] ShipInputControls inputControls;

    [SerializeField]
    [Range(1000f, 10000f)]
    float thrustForce = 7500f,
          pitchForce = 6000f,
          rollForce = 1000f,
          yawForce = 2000f;

    Rigidbody rigidBody;
    [SerializeField] List<ShipEngine> engines;
    [SerializeField] List<Blaster> blasters;



    [SerializeField] AnimateCockpitControls cockpitAnimationControls;
    
    float pitchAmount = 0f,
          rollAmount = 0f,
          yawAmount = 0f;

    IMovementControls MovementInput => inputControls.MovementControls;
    IWeaponControls WeaponInput => inputControls.WeaponControls;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        foreach(ShipEngine engine in engines)
        {
            engine.Init(MovementInput, rigidBody, thrustForce / engines.Count);
        }  
        if(cockpitAnimationControls != null)
        {
            cockpitAnimationControls.Init(MovementInput);
        }
        

        foreach (Blaster blaster in blasters)
        {
            blaster.Init(WeaponInput);
        }
    }

    void Update()
    {
        rollAmount = MovementInput.RollAmount;
        yawAmount = MovementInput.YawAmount;
        pitchAmount = MovementInput.PitchAmount;
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
    }
}
