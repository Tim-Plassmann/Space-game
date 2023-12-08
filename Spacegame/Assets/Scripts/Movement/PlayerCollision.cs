using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public HealthSystem playerHealthSystem;
    private bool hasCollided = false;
    public int collisionsDelay = 2;

    void Start()
    {
        
        playerHealthSystem = GetComponent<HealthSystem>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (!hasCollided)
        {
            
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Player collided with enemy.");

                
                DamageInfo damageInfo = collision.gameObject.GetComponent<DamageInfo>();
                int damageAmount = (damageInfo != null)
                    ? damageInfo.CalculateDamage(collision.gameObject.GetComponent<Rigidbody>())
                    : 10; 

                HealthSystem enemyHealthSystem = collision.gameObject.GetComponent<HealthSystem>();
                if (enemyHealthSystem != null)
                {
                    enemyHealthSystem.TakeDamage(damageAmount);
                    Debug.Log("Enemy health: " + enemyHealthSystem.GetCurrentHealth());
                    if (enemyHealthSystem = null)
                    {
                        Destroy(gameObject);
                    }
                }

                
                playerHealthSystem.TakeDamage(damageAmount);
                Debug.Log("Player health: " + playerHealthSystem.GetCurrentHealth());

                
                hasCollided = true;

                
                StartCoroutine(ResetCollisionFlagAfterDelay(2f));
            }
            // Check if the collided object has the tag "Objects"
            if (collision.gameObject.CompareTag("Object"))
            {
                // Get the Rigidbody component of the collided object (asteroid)
                Rigidbody asteroidRb = collision.gameObject.GetComponent<Rigidbody>();

                // Check if the Rigidbody component is not null
                if (asteroidRb != null)
                {
                    // Get the velocity of the spaceship
                    Rigidbody spaceshipRb = GetComponent<Rigidbody>();

                    // Check if the spaceship's Rigidbody component is not null
                    if (spaceshipRb != null)
                    {
                        // Set the asteroid's velocity to match the spaceship's velocity
                        asteroidRb.velocity = spaceshipRb.velocity;
                    }
                }
            }
        }
    }

    IEnumerator ResetCollisionFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        hasCollided = false;
    }
}
