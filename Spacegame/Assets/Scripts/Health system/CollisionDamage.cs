using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CollisionDamage : MonoBehaviour
{
    private HealthSystem healthSystem;
    private bool hasCollided = false;

    private async void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            // Check if the collision involves any tag
            foreach (string tag in GetAllTags())
            {
                if (collision.gameObject.CompareTag(tag))
                {
                    Debug.Log($"Collided with an object with tag: {tag}");

                    // Get the HealthSystem component of the collided object
                    healthSystem = GetComponent<HealthSystem>();

                    // Check if the object has a HealthSystem component
                    if (healthSystem != null)
                    {
                        healthSystem.TakeDamage(10);
                                                
                        Debug.Log("du har taget skade");
                        continue;
                    }
                    await DelayedAction(20000);
                }
            }
        }
    }
    static async Task DelayedAction(int delay)
    {
        // Code to execute after the delay goes here
        await Task.Delay(delay);
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
        // Convert HashSet to an array
        string[] tagsArray = new string[uniqueTags.Count];
        uniqueTags.CopyTo(tagsArray);

        return tagsArray;
    }
}
