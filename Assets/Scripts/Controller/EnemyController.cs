using System;
using System.Collections;
using UnityEngine;

public class EnemyController : AttackController {
    [SerializeField] private float attackRange;
    [SerializeField] private float followSpeed;
    [SerializeField] private float timeToAttack = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AttackLineIndicator attackLineIndicator;

    float elapsedTimeSinceLastAttack = 0;
    float Xval;

    Vector2 attackPosition;
    Vector2 targetPosition;
    Transform playerTransform;

    bool headsup = false;

    void Start() {
        playerTransform = GameObject.FindAnyObjectByType<Player>().transform;
        elapsedTimeSinceLastAttack = timeToAttack;
    }
    void OnDisable() {
        attackLineIndicator.ResetAttackLine();
        headsup = false;
        isAttacking = false;
    }
    void Update() {
        if (isAttacking) {
            if (IsReachedAttackDestination(attackPosition)) {
                isAttacking = false;
                headsup = isAttacking;
                elapsedTimeSinceLastAttack = timeToAttack;
                attackLineIndicator.ResetAttackLine();
                AttackCooldownEvent();
            } else {
                Attack(attackPosition);
            }
            return;
        }

        if (OnRange && !OnCooldown && !headsup) {
            StartCoroutine(AttackHeadsUp());
            return;
        }

        if (headsup) return;
        Follow();
        AttackCooldown();
        targetPosition = GetTargetPosition();

        spriteRenderer.flipX = Xval > 0.01f ? true : false;
        Xval = transform.position.x - targetPosition.x;
    }

    IEnumerator AttackHeadsUp() {
        headsup = true;
        ReadyAttackEvent();
        attackPosition = GetTargetPosition();
        attackLineIndicator.SetAttackLine(transform.position, attackPosition);
        yield return new WaitForSeconds(.5f);
        StarAttacking();
    }
    private void StarAttacking() {
        AttackReleaseEvent();
        isAttacking = true;
    }
    private void Follow() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private Vector2 GetTargetPosition() {
        return playerTransform.position;
    }
    private bool OnRange => Vector2.Distance(transform.position, playerTransform.position) < attackRange;
    private void AttackCooldown() {
        if (elapsedTimeSinceLastAttack > 0) {
            elapsedTimeSinceLastAttack -= Time.deltaTime;
        }
    }
    private bool OnCooldown => elapsedTimeSinceLastAttack > 0;

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}