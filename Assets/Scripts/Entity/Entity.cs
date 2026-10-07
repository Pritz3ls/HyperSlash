using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable {
    [SerializeField] public int health;
    private bool isDead = false;
    public event Action OnDamage;
    public event Action OnDeath;
    void OnEnable() {
        health = 1;
        isDead = false;
    }
    public virtual void TakeDamage(int damage) {
        if (isDead) return;
        health -= damage;
        OnDamage?.Invoke();
        if (health <= 0) {
            isDead = true;
            OnDeath?.Invoke();
            StartCoroutine(Disable(.1f));
        }
    }

    IEnumerator Disable(float time = 0) {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
    }
}