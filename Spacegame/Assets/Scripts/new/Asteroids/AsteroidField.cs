using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    [SerializeField][Range(100, 1000)] private int asteroidCount = 500;
    [SerializeField][Range(100f, 500f)] private float radius = 300;
    [SerializeField][Range(1f, 5f)] float maxScale = 1;
    [SerializeField] private List<GameObject> asteroidPrefabs;

    private Transform _transform;


    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < asteroidCount; i++)
        {
            GameObject asteroid = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)], transform.position, Quaternion.identity);
            float scale = Random.Range(0.5f, maxScale);
            asteroid.transform.localScale = new Vector3(scale, scale, scale);
            asteroid.transform.position += Random.insideUnitSphere * radius;
            asteroid.GetComponent<Rigidbody>()?.AddTorque(Random.insideUnitCircle * Random.Range(0f, 50f));
        }
    }
}