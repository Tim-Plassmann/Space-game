using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementInput : MonoBehaviour
{
    [SerializeField] ShipInputManager.InputType inputType = ShipInputManager.InputType.HumanDesktop;

    public IMovementControls MovementControls { get; private set; }
    
    void Start()
    {
        MovementControls = ShipInputManager.GetInputcontrols(inputType);
    }

   
    void OnDestroy()
    {
        MovementControls = null;
    }
}
