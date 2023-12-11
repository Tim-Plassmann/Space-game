using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float rotationSpeed = 3f;
    public float damping = 0.3f; 

    private Vector3 velocity;

    private void Update()
    {
        if (player != null)
        {
            
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, damping, speed);

            
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
