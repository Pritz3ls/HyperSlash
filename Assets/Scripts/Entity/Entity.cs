using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable {
    [SerializeField] public int health;
    private bool isDead = false;
    public event Action OnDamage;
    public event Action<Vector2> OnDamageSource; // For Bounce
    public event Action OnDeath;
    void OnEnable() {
        health = 1;
        isDead = false;
    }
    public virtual void TakeDamage(int damage, Vector2 source) {
        if (isDead) return;
        health -= damage;
        OnDamage?.Invoke();
        OnDamageSource?.Invoke(source);
        if (health <= 0) {
            isDead = true;
            OnDeath?.Invoke();
        }
    }

    public void DelayDisable() {
        StartCoroutine(Disable(.1f));
    }

    IEnumerator Disable(float time = 0) {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
    }
}