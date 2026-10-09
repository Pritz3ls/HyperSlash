using UnityEngine;

public class Player : Entity {
    [SerializeField] private DeathBounce deathBounce;
    [SerializeField] private GameObject deathImpact;
    [SerializeField] private GameObject slashSprite;
    private void Start() {
        OnDamageSource += DeathOnDamage;
        OnDeath += Death;
        GameManager.Instance.OnGameRestart += Revive;
    }

    private void Revive() {
        slashSprite.SetActive(true);
        deathImpact.SetActive(false);
        deathBounce.ResetCorpse();
    }

    private void Death() {
        Debug.Log("Death");
        deathImpact.SetActive(true);
        DelayDisable();

        SFXManager.Instance.PlaySFX(1);
    }
    private void DeathOnDamage(Vector2 source) {
        slashSprite.SetActive(false);
        FreezeFrame.Instance.Freeze(.5f, this.transform);
        deathBounce.OnDeath(source);
    }
}