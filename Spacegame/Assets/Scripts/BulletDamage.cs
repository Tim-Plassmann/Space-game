using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private int damage = 10;

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hit is the player
        if (other.CompareTag("Player"))
        {
            // Ignore collisions with the player
            return;
        }

        // Check if the object hit has a HealthSystem component
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            // Deal damage to the object
            healthSystem.TakeDamage(damage);

            // Destroy the bullet
            Destroy(gameObject);

            Debug.Log("Bullet hit object with HealthSystem. Dealt damage: " + damage);
        }
        else
        {
            Debug.Log("Bullet hit object without HealthSystem.");
        }
    }
}
