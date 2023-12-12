using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    [SerializeField] GameObject thruster;

    IMovementControls ShipMovementControls;
    Rigidbody rigidbody;
    float thrustForce;
    float thrustAmount = 0f;

    bool ThrusterEnabled => !Mathf.Approximately(0f, ShipMovementControls.ThrustAmount);
    
    void Start()
    {
        
    }

    
    void Update()
    {
        ActivateThrusters();  
    }

    void FixedUpdate() 
    {
        if (!ThrusterEnabled) return; 
        rigidbody.AddForce(transform.forward * (thrustAmount * Time.fixedDeltaTime));
    }

    void ActivateThrusters()
    {
        thruster.SetActive(ThrusterEnabled);
        if (!ThrusterEnabled) return;
        thrustAmount = thrustForce * ShipMovementControls.ThrustAmount;
    }

    public void Init(IMovementControls movementcontrols, Rigidbody rb, float ThrustForce)
    {
        rigidbody = rb;
        ShipMovementControls = movementcontrols;
        thrustForce = ThrustForce;
    }
}
