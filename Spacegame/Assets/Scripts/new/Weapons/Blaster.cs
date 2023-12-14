using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField]
    Projectile projectilePrefab;

    [SerializeField]
    Transform muzzle;

    float coolDownTime = 0.25f;
    private int LaunchForce, Damage;
    private float Duration;
    IWeaponControls WeaponInput;

    bool cantFire
    {
        get 
        {
            CoolDown -= Time.deltaTime;
            return CoolDown <= 0f;
        }
    }
    float CoolDown;

    void Update()
    {
        if (WeaponInput == null) return;
        if (cantFire && WeaponInput.PrimaryFired) 
        {
            FireProjectile();
        }
    }
    public void Init(IWeaponControls weaponInput, float coolDown, int launchForce, float duration, int damage)
    {
        WeaponInput = weaponInput;
        coolDownTime = coolDown;
        LaunchForce = launchForce;
        Duration = duration;
        Damage = damage;
    }

    void FireProjectile()
    {
        CoolDown = coolDownTime;
        Projectile projectile = Instantiate(projectilePrefab, muzzle.position, transform.rotation);
        projectile.gameObject.SetActive(false);
        projectile.Init(LaunchForce, Damage, Duration);
        projectile.gameObject.SetActive(true);
    }

    
}
