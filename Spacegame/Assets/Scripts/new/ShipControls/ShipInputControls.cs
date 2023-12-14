using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInputControls : MonoBehaviour
{
    [SerializeField] ShipInputManager.InputType inputType = ShipInputManager.InputType.HumanDesktop;

    public IMovementControls MovementControls { get; private set; }
    public IWeaponControls WeaponControls { get; private set; }
    
    void Start()
    {
        MovementControls = ShipInputManager.GetMovementControls(inputType);
        WeaponControls = ShipInputManager.GetWeaponControls(inputType);
    }

   
    void OnDestroy()
    {
        MovementControls = null;
    }
}
