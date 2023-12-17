using System;
using UnityEngine;

[Serializable]
public class DesktopWeaponControls : WeaponControllsBase
{
    public override bool PrimaryFired => Input.GetMouseButton(0);
    public override bool SecondaryFired => Input.GetMouseButton(1);
}