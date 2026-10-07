using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable {
    [SerializeField] private float health;
    public event Action OnDamage;
    public event Action OnDeath;
    public void TakeDamage(float damage) {
        health -= damage;
        OnDamage?.Invoke();
        if (health <= 0) {
            OnDeath?.Invoke();
            gameObject.SetActive(false);
        }
    }
}