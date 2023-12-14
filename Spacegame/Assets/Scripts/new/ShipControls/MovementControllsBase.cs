using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementControllsBase : IMovementControls
{
    
    public abstract float YawAmount { get; }
    public abstract float PitchAmount { get; }
    public abstract float RollAmount { get; }
    public abstract float ThrustAmount { get; }

}

public abstract class WeaponControlsBase : IWeaponControls
{
    public abstract bool PrimaryFired { get; }
    public abstract bool SecondaryFired { get; }
}
