using UnityEngine;

public class SpaceshipShooting : MonoBehaviour
{
    public Transform[] firePoints;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public int bulletDamage = 10;
    public Vector3[] offsets;
    public float shootCooldown = 0.5f; // Cooldown time in seconds

    private float lastShootTime; // Time when the last shot occurred

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time - lastShootTime > shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time; // Record the time of the last shot
        }
    }

    void Shoot()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            Transform firePoint = firePoints[i];
            Vector3 offset = i < offsets.Length ? offsets[i] : Vector3.zero;

            Vector3 spawnPosition = firePoint.position +
                                   firePoint.forward * offset.z +
                                   firePoint.up * offset.y +
                                   firePoint.right * offset.x;

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firePoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                bulletRb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
            }

            BulletDamage bulletDamageScript = bullet.GetComponent<BulletDamage>();
            if (bulletDamageScript != null)
            {
                bulletDamageScript.SetDamage(bulletDamage);
            }
        }
    }
}
