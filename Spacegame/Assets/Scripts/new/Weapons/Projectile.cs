using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    [Range(5000f, 25000f)] 
    float launchForce = 10000f;

    [SerializeField]
    [Range(5000, 25000)]
    int damage = 100;


    [SerializeField]
    [Range(2f, 10f)]
    float range = 5f;


    bool OutOfFuel
    {
        get
        {
            duration -= Time.deltaTime;
            return duration <= 0f;
        }
    }
    Rigidbody rigidBody;
    float duration;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody> ();
    }

    void OnEnable()
    {
        rigidBody.AddForce(launchForce * transform.forward);
        duration = range;
    }

    void Update()
    {
        if (OutOfFuel) Destroy(gameObject);
    }

    void OnColisionEnter(Collision collision)
    {
        Debug.Log($"Projectilet har ramt {collision.collider.name}");
    }
}
