using UnityEngine;

public class Enemy : Entity {
    private void Start() {
        OnDamage += FreezeOnDamage;
        OnDeath += Death;
    }
    private void Death() {
        ScoreManager.Instance.AddScore();
    }
    private void FreezeOnDamage() {
        FreezeFrame.Instance.Freeze(.2f);
    }
}