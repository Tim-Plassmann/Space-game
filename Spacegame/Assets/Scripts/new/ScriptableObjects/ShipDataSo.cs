using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "3D Space Shooter/ship Data", order = 1)]

public class ShipDataSo : ScriptableObject

{
    public float ThrustForce => thrustForce;
    public float PitchForce => pitchForce;
    public float RollForce => rollForce;
    public float YawForce => yawForce;
    public int ShieldStrength => shieldStrength;
    public int ShieldRegenAmount => shieldRegenAmount;
    public int MaxHealth => maxHealth;
    public int BlasterDamage => blasterDamage;
    public float BlasterProjectileDuration => blasterProjectileDuration;
    public float BlasterCoolDown => blasterCoolDown;
    public int BlasterLaunchForce => blasterLaunchForce;


    [BoxGroup("Ship movement values")][SerializeField][Range(1000f, 50000f)]
    float thrustForce = 7500f,
          pitchForce = 6000f,
          rollForce = 1000f,
          yawForce = 2000f;


    [BoxGroup("Ship component values")]
    [SerializeField]
    int shieldStrength = 5000,
        shieldRegenAmount = 100,
        maxHealth = 5000;

    [BoxGroup("Ship component values")]
    [SerializeField]
    [Range(10, 1000)]
    int blasterDamage = 100;


    [BoxGroup("Ship component values")]
    [SerializeField]
    [Range(5000f, 25000f)]
    int blasterLaunchForce = 10000;


    [BoxGroup("Ship component values")]
    [SerializeField]
    float blasterCoolDown = 0.25f;

    [BoxGroup("Ship component values")]
    [SerializeField]
    [Range(2f, 10f)]
    float blasterProjectileDuration = 2f;


}
