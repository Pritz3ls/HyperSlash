using UnityEngine;

public class Enemy : Entity {
    [SerializeField] private FlashEffect flashEffect;
    private void Start() {
        OnDamage += FreezeOnDamage;
        OnDeath += Death;
    }
    private void Death() {
        ScoreManager.Instance.AddScore();
        BloodManager.Instance.SpawnBloodPool(transform.position);
        BloodManager.Instance.SpawnGib(transform.position);
        DelayDisable();
    }
    private void FreezeOnDamage() {
        FreezeFrame.Instance.Freeze(.1f, this.transform);
        flashEffect.Flash();
    }
    public void Sepuku() {
        BloodManager.Instance.SpawnBloodPool(transform.position);
        BloodManager.Instance.SpawnGib(transform.position);
        DelayDisable();
    }
}