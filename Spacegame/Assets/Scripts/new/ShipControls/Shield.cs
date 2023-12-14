using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField][Range(0.25f, 1f)] private float fadeOutTime = 0.5f;
    [SerializeField] private float minIntensity = -10f, maxIntensity = 0f;
    [SerializeField] private int maxHealth = 5000;
    [SerializeField] private GameObject explosionPrefab;

    private Renderer renderer;
    private Color baseColor;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private int health;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        baseColor = renderer.material.color;
        health = maxHealth;
        
    }

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyShields();
            return;
        }
        StartCoroutine(FlashAndFadeShields());
    }

    private void DestroyShields()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        StopAllCoroutines();
        Destroy(gameObject);
    }

    IEnumerator FlashAndFadeShields()
    {
        Color shieldColor;
        Color emissionColor = renderer.material.GetColor(EmissionColor);
        float currentTime = 0f;
        float intensity = maxIntensity;
        while (currentTime < fadeOutTime)
        {
            shieldColor = Color.Lerp(flashColor, baseColor, currentTime / fadeOutTime);
            intensity = Mathf.Lerp(maxIntensity, minIntensity, currentTime / fadeOutTime);
            ChangeShieldColor(shieldColor, emissionColor * Mathf.Pow(2, intensity));
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield break;
    }

    private void ChangeShieldColor(Color shieldColor, Color emissionColor)
    {
        renderer.material.color = shieldColor;
        renderer.material.SetColor(EmissionColor, emissionColor);
    }
}