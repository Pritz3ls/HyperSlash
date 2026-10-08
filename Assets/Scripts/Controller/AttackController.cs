using System;
using UnityEngine;

public class AttackController : MonoBehaviour {
    [SerializeField] private float attackSpeed;
    [SerializeField] public bool isAttacking;
    [SerializeField] private LayerMask damageMask;
    [SerializeField] public Vector2 attackOffset;
    [SerializeField] public float attackDamageRadius = 0.5f;

    public event Action OnAttackReady;
    public event Action OnAttackRelease;
    public event Action OnAttackCooldown;

    public void Attack(Vector2 targetPosition) {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, attackSpeed * Time.deltaTime);
        if (isAttacking) {
            Collider2D col = Physics2D.OverlapCircle((Vector2)transform.position + attackOffset, attackDamageRadius, damageMask);
            if (col == null) return;
            if (col.TryGetComponent<IDamageable>(out IDamageable component)) {
                component.TakeDamage(5, transform.position);
            }
        }
    }
    public bool IsReachedAttackDestination(Vector2 targetPosition) {
        return Vector2.Distance(transform.position, targetPosition) < 0.1f;
    }

    public void ReadyAttackEvent() {
        OnAttackReady?.Invoke();
    }
    public void AttackReleaseEvent() {
        OnAttackRelease?.Invoke();
    }
    public void AttackCooldownEvent() {
        OnAttackCooldown?.Invoke();
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere((Vector2)transform.position + attackOffset, attackDamageRadius);
    }
}