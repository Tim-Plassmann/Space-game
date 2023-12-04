using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float rotationSpeed = 3f;

    private void Update()
    {
        if (player != null)
        {
            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Rotate towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
