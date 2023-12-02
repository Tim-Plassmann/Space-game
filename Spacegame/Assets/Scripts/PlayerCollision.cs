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
        }
    }

    IEnumerator ResetCollisionFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        hasCollided = false;
    }
}
