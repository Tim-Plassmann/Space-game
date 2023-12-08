using UnityEngine;

public class DamageInfo : MonoBehaviour
{
    public float damageMultiplier = 0.1f;

    public int CalculateDamage(Rigidbody otherRigidbody)
    {
        if (otherRigidbody != null)
        {
            float mass = otherRigidbody.mass;
            int damageAmount = Mathf.RoundToInt(mass * damageMultiplier);
            return Mathf.Max(damageAmount, 1);
        }

        return 1;
    }
}
