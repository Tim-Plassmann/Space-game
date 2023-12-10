using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementInput : MonoBehaviour
{
    [SerializeField] ShipInputManager.InputType inputType = ShipInputManager.InputType.HumanDesktop;

    public IMovementControls MovementControls { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        MovementControls = ShipInputManager.GetInputcontrols(inputType);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        MovementControls = null;
    }
}
