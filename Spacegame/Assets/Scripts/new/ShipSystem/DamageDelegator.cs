using System.Collections.Generic;
using UnityEngine;

public class DamageDelegator : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject[] damageDelegates;

    List<IDamageable> damageReceivers;

    void Awake()
    {
        if (damageDelegates == null) return;
        damageReceivers = new List<IDamageable>();
        foreach (GameObject damageDelegate in damageDelegates)
        {
            if (damageDelegate.TryGetComponent<IDamageable>(out var damageable))
            {
                damageReceivers.Add(damageable);
            }
        }
    }
    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        if (damageReceivers == null) return;
        foreach (IDamageable damageable in damageReceivers) 
        {
            damageable.TakeDamage(damage, hitPosition);
        }
    }
}

