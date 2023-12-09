using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves any tag
        foreach (string tag in GetAllTags())
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Debug.Log($"Collided with an object with tag: {tag}");

                // You can access the collided GameObject or perform actions based on the collision
                GameObject collidedObject = collision.gameObject;

                healthSystem.TakeDamage(1);
                StartCoroutine(Delay());
                // Perform actions based on the collision
                // For example, you might want to damage the object based on its tag
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.4f);

        // Code to execute after the delay goes here
        Debug.Log("Delay completed");
    }

    private string[] GetAllTags()
    {
        // Find all objects with a Collider component
        Collider[] colliders = FindObjectsOfType<Collider>();

        // Use a HashSet to ensure unique tags
        HashSet<string> uniqueTags = new HashSet<string>();

        // Collect unique tags from colliders
        foreach (Collider collider in colliders)
        {
            if (collider.tag != "Untagged")
            {
                uniqueTags.Add(collider.tag);
            }
            else
            {
                continue;
            }
        }
        Debug.Log(colliders);

        // Convert HashSet to an array
        string[] tagsArray = new string[uniqueTags.Count];
        uniqueTags.CopyTo(tagsArray);

        return tagsArray;
    }
}
