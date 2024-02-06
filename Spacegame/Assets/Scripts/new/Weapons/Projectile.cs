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

    Rigidbody _rigidBody;
    float Duration;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        _rigidBody.AddForce(LaunchForce * transform.forward);
        Duration = Range;
    }

    private void OnDisable()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (OutOfFuel) Destroy(gameObject);
    }

    public void Init(int launchForce, int damage, float range, Vector3 velocity, Vector3 angularVelocity)
    {
        LaunchForce = launchForce;
        Damage = damage;
        Range = range;
        _rigidBody.velocity = velocity;
        _rigidBody.angularVelocity = angularVelocity;
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