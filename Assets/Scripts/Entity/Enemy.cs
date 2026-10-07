using UnityEngine;

public class Enemy : Entity {
    [SerializeField] private FlashEffect flashEffect;
    private void Start() {
        OnDamage += FreezeOnDamage;
        OnDeath += Death;
    }
    private void Death() {
        ScoreManager.Instance.AddScore();
    }
    private void FreezeOnDamage() {
        FreezeFrame.Instance.Freeze(.2f);
        flashEffect.Flash();
    }
}