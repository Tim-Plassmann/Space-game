using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    float LaunchForce;
    int Damage;
    float Range;
    [SerializeField] private Detonator hitEffect;

    bool OutOfFuel
    {
        get
        {
            Duration -= Time.deltaTime;
            return Duration <= 0f;
        }
    }

    Rigidbody RigidBody;
    float Duration;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        RigidBody.AddForce(LaunchForce * transform.forward);
        Duration = Range;
    }

    private void OnDisable()
    {
        RigidBody.velocity = Vector3.zero;
        RigidBody.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (OutOfFuel) Destroy(gameObject);
    }

    public void Init(int launchForce, int damage, float range)
    {
        LaunchForce = launchForce;
        Damage = damage;
        Range = range;
    }

    void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.collider.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Vector3 hitPosition = collision.GetContact(0).point;
            damageable.TakeDamage(Damage, hitPosition);
        }

        if(hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}