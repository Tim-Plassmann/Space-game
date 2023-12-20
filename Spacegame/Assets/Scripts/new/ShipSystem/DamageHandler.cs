using UnityEngine.Events;
using UnityEngine;

public class DamageHandler : MonoBehaviour, IDamageable
{
    UnityEvent healthChangedEvent;
    UnityEvent objectDestroyedEvent;
    public int MaxHealth { get; private set; }
    public int Health {  get; private set; }
    [SerializeField] GameObject explosionPrefab;

    public UnityEvent HealthChanged => healthChangedEvent ??= new UnityEvent();
    public UnityEvent ObjectDestroyed => objectDestroyedEvent ??= new UnityEvent();

    public void Init(int maxHealth)
    {
        Health = MaxHealth = maxHealth;
        HealthChanged.Invoke();
    }


    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        Health -= damage; 
        HealthChanged.Invoke();
        if (Health >= 0) return;
        
        if (explosionPrefab)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        ObjectDestroyed.Invoke();        
    }
}
