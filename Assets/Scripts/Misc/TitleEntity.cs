using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEntity : Entity {
    [SerializeField] private ParticleSystem gib;
    [SerializeField] private GameObject visual;

    private void Start() {
        OnDamage += ShakeOnDamage;
        OnDeath += Death;
    }
    private void Death() {
        visual.SetActive(false);
        gib.Play();

        SFXManager.Instance.PlaySFX(6);
    }
    private void ShakeOnDamage() {
        CameraShaker.instance.StartShake(.1f, .2f);
    }
}
