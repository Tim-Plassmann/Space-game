using System;
using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] DamageHandler _damagehandler;
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField][Range(0.25f, 1f)] private float fadeOutTime = 0.5f;
    [SerializeField] private float minIntensity = -10f, maxIntensity = 0f;

    Renderer renderer;
    Color baseColor;
    static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        baseColor = renderer.material.color;
        
    }

    public void Init(int shieldStrength)
    {
        _damagehandler.Init(shieldStrength);
    }

    void OnEnable()
    {
        _damagehandler.HealthChanged.AddListener(OnHealthChanged);
        _damagehandler.ObjectDestroyed.AddListener(DestroyShields);
    }

    private void OnDisable()
    {
        _damagehandler.HealthChanged.RemoveListener(OnHealthChanged);
        _damagehandler.ObjectDestroyed.RemoveListener(DestroyShields);
    }
    void OnHealthChanged()
    {
        StartCoroutine(FlashAndFadeShields());
    }

    private void DestroyShields()
    {
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