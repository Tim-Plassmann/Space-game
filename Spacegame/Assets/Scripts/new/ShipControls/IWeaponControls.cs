using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponControls
{
    bool PrimaryFired { get; }
    bool SecondaryFired { get; }
}
