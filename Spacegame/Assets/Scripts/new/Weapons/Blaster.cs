using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField]
    Projectile projectilePrefab;

    [SerializeField]
    Transform muzzle;

    [SerializeField]
    [Range(0f, 5f)]
    float coolDownTime = 0.25f;

    private IWeaponControls WeaponInput;

    bool cantFire
    {
        get 
        {
            coolDown -= Time.deltaTime;
            return coolDown <= 0f;
        }
    }
    float coolDown;

    void Update()
    {
        if (WeaponInput == null) return;
        if (cantFire && WeaponInput.PrimaryFired) 
        {
            FireProjectile();
        }
    }
    public void Init(IWeaponControls weaponInput)
    {
        WeaponInput = weaponInput;
    }

    void FireProjectile()
    {
        coolDown = coolDownTime;
        Instantiate(projectilePrefab, muzzle.position, transform.rotation);
    }

    
}
