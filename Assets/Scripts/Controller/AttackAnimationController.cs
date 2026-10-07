using UnityEngine;

public class AttackAnimationController : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private AttackController attackController;

    public static int Ready = Animator.StringToHash("ready");
    public static int Attack = Animator.StringToHash("attack");
    public static int Idle = Animator.StringToHash("idle");

    private void Start() {
        attackController.OnAttackReady += ReadyAnimation;
        attackController.OnAttackRelease += AttackAnimation;
        attackController.OnAttackCooldown += IdleAnimation;
    }

    private void OnDisable() {
        attackController.OnAttackReady -= ReadyAnimation;
        attackController.OnAttackRelease -= AttackAnimation;
        attackController.OnAttackCooldown -= IdleAnimation;
    }

    private void ReadyAnimation() {
        animator.CrossFade(Ready, 0, 0);
    }
    private void AttackAnimation() {
        animator.CrossFade(Attack, 0, 0);
    }
    private void IdleAnimation() {
        animator.CrossFade(Idle, 0, 0);
    }
}