using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponControllsBase : MonoBehaviour, IWeaponControls

{
    public abstract bool PrimaryFired { get; }
    public abstract bool SecondaryFired { get; }
}

