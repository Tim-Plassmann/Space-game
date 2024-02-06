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
    float Duration;
    IWeaponControls WeaponInput;
    float CoolDown;
    Rigidbody _rigidbody;

    bool cantFire
    {
        get 
        {
            CoolDown -= Time.deltaTime;
            return CoolDown <= 0f;
        }
    }
    

    void Update()
    {
        if (WeaponInput == null) return;
        if (cantFire && WeaponInput.PrimaryFired) 
        {
            FireProjectile();
        }
    }
    public void Init(IWeaponControls weaponInput, float coolDown, int launchForce, float duration, int damage, Rigidbody rigidBody)
    {
        WeaponInput = weaponInput;
        coolDownTime = coolDown;
        LaunchForce = launchForce;
        Duration = duration;
        Damage = damage;
        _rigidbody = rigidBody;
    }

    void FireProjectile()
    {
        CoolDown = coolDownTime;
        Projectile projectile = Instantiate(projectilePrefab, muzzle.position, transform.rotation);
        projectile.gameObject.SetActive(false);
        projectile.Init(LaunchForce, Damage, Duration, _rigidbody.velocity, _rigidbody.angularVelocity);
        projectile.gameObject.SetActive(true);
    }

    
}
