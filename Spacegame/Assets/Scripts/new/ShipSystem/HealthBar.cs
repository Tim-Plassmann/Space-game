using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBarImage;
    [SerializeField] float updateRate = 1.0f;
    [SerializeField] DamageHandler damageHandler;

    Transform transForm;
    Camera camera;
    float targetFillAmount;

    void Awake()
    {
        transForm = transform;
        camera = Camera.main;
        if (damageHandler == null) return;
        targetFillAmount = 1;
        UpdateHealthBar();

    }

    void OnEnable()
    {
        if (damageHandler == null) return;
        damageHandler.HealthChanged.AddListener(UpdateHealthBar);
    }

    void LateUpdate()
    {
        transForm.LookAt(camera.transform);
        if (damageHandler == null || healthBarImage == null) return;
        if (Mathf.Approximately(healthBarImage.fillAmount, targetFillAmount)) return;
        healthBarImage.fillAmount = Mathf.MoveTowards(healthBarImage.fillAmount, targetFillAmount, updateRate * Time.deltaTime);
    }

    void OnDisable()
    {
        if (damageHandler == null) return;
        damageHandler.HealthChanged.RemoveListener(UpdateHealthBar);
    }
    void UpdateHealthBar()
    {
        if (damageHandler == null) return;
        if (damageHandler.Health == 0)
        {
            targetFillAmount = 0;
            return;
        }
        targetFillAmount = (float)damageHandler.Health / (float)damageHandler.MaxHealth;
    }
}