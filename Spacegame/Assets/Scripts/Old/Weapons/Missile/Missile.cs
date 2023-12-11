using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 3f;
    public float rotationDamping = 0.3f;

    private Transform target;

    // Set the target for the missile to follow
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target != null)
        {
            // Rotate the missile to face the target
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                rotationDamping * rotationSpeed * Time.deltaTime
            );

            // Move the missile forward in its forward direction
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            // If there's no target, self-destruct the missile
            Destroy(gameObject);
            Debug.Log("missilet fandt ikke nogle targets");
        }
    }
}
