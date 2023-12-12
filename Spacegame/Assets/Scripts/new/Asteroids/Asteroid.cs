using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    [SerializeField] private FracturedAsteroid fracturedAsteroidPrefab;
    [SerializeField] private Detonator explosionPrefab;

    private Transform transForm;

    private void Awake() 
    {
        transForm = transform;  
    }
    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        FractureAsteroid(hitPosition);
    }

    private void FractureAsteroid(Vector3 hitPosition)
    {
        if (fracturedAsteroidPrefab != null)
        {
            Instantiate(fracturedAsteroidPrefab, transForm.position, transForm.rotation);
        }

        if (explosionPrefab != null) 
        {
            Instantiate(explosionPrefab, hitPosition, Quaternion.identity);       
        }

        Destroy(gameObject);
    }
}


