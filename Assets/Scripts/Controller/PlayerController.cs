using System;
using UnityEngine;

public class PlayerController : AttackController {
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AttackLineIndicator attackLineIndicator;
    Vector2 lastPosition;
    Vector2 mouseWorldPos;
    Vector2 targetPosition;

    private float Xval;

    void Update() {
        spriteRenderer.flipX = Xval > 0.01f ? true : false;

        // Vector2 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0)) {
            ReadyAttackEvent();
        }
        if (Input.GetMouseButton(0)) {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Xval = transform.position.x - mouseWorldPos.x;
            attackLineIndicator.SetAttackLine(transform.position, mouseWorldPos);

        } else if (Input.GetMouseButtonUp(0)) {
            lastPosition = transform.position;
            targetPosition = mouseWorldPos;
            AttackReleaseEvent();
            isAttacking = true;
            attackLineIndicator.ResetAttackLine();

            SFXManager.Instance.PlaySFX(0);
        }

        if (isAttacking) {
            if (IsReachedAttackDestination(targetPosition)) {
                isAttacking = false;
                AttackCooldownEvent();
            } else {
                Attack(targetPosition);
            }
            return;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, targetPosition);
    }

    public bool IsAttacking => isAttacking;
    public Vector2 GetLastPosition => lastPosition;
}
