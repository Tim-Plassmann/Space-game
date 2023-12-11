using UnityEngine;

public class SmoothPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    public float pitchDamping = 5f;
    public float yawDamping = 5f;
    public float speedDamping = 2f;

    private float currentPitchSpeed;
    private float currentYawSpeed;
    private float currentForwardSpeed;

    public Missile missilePrefab;
    public Transform launchPointObject;
    public Transform targetObject; 
    public string enemyTag = "Enemy";
    public Vector3 launchOffset = new Vector3(2.35f, 74.87f, 78.57f);

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
        if (Input.GetButtonDown("Missile"))    
        {
            LaunchMissile();
            Debug.Log("Missile skudt af!");
        }
    }

    private void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float rollInput = Input.GetAxis("Horizontal");

        // Update forward movement based on input
        currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, verticalInput * moveSpeed, Time.deltaTime * speedDamping);
        Vector3 moveAmount = transform.forward * currentForwardSpeed * Time.deltaTime;
        transform.Translate(moveAmount, Space.World);

        // Roll the player based on input
        float rollAmount = rollInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, -rollAmount);
    }

    private void RotatePlayer()
    {
        float pitchInput = Input.GetAxis("Pitch");
        float yawInput = Input.GetAxis("Yaw");

        // Update pitch and yaw speed with damping
        currentPitchSpeed = Mathf.Lerp(currentPitchSpeed, pitchInput * rotationSpeed, Time.deltaTime * pitchDamping);
        currentYawSpeed = Mathf.Lerp(currentYawSpeed, yawInput * rotationSpeed, Time.deltaTime * yawDamping);

        // Apply rotation without affecting forward movement
        transform.localRotation *= Quaternion.Euler(Vector3.left * currentPitchSpeed * Time.deltaTime);
        transform.rotation *= Quaternion.Euler(Vector3.up * currentYawSpeed * Time.deltaTime);
    }
    private void LaunchMissile()
    {
        // Find all game objects with the specified enemy tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // If there are no enemies, do not launch a missile
        if (enemies.Length == 0)
        {
            Debug.LogWarning("No enemies found.");
            return;
        }

        // Find the closest enemy
        GameObject closestEnemy = GetClosestEnemy(enemies);

        // If a closest enemy is found, launch a missile
        if (closestEnemy != null)
        {
            GameObject missileObject = Instantiate(missilePrefab.gameObject, launchPointObject.position, launchPointObject.rotation);
            Missile newMissile = missileObject.GetComponent<Missile>();

            if (newMissile != null)
            {
                // Set the target for the missile
                newMissile.SetTarget(closestEnemy.transform);
            }
            else
            {
                // Handle the case where the instantiated object doesn't have a Missile component
                Debug.LogError("Instantiated object is missing Missile component.");
                Destroy(missileObject);
            }
        }
    }

    private GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
