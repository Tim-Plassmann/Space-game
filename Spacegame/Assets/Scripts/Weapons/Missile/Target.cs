using UnityEngine;

public class Target : MonoBehaviour
{
    public HealthSystem playerHealthSystem;
    public int damageInfo = 50;
    private bool hasCollided = false;

    void Start()
    {
        playerHealthSystem = GetComponent<HealthSystem>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (otherRigidbody != null)
            {
                Debug.Log("Missile has hit enemy.");

                DamageInfo damageInfo = GetComponent<DamageInfo>();
                int damageAmount = (damageInfo != null) ? damageInfo.CalculateDamage(otherRigidbody) : 10;

                // Assuming that you have a HealthSystem component on the same object
                HealthSystem enemyHealthSystem = GetComponent<HealthSystem>();

                if (enemyHealthSystem != null)
                {
                    enemyHealthSystem.TakeDamage(damageAmount);
                    Debug.Log("Enemy health: " + enemyHealthSystem.GetCurrentHealth());

                    if (enemyHealthSystem.GetCurrentHealth() <= 0)
                    {
                        Destroy(gameObject); // Destroy the missile if the enemy is defeated
                    }
                }
            }

            //Destroy(gameObject); // Always destroy the missile upon collision
        }
    }

}

