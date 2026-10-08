using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImpactFrame))]
public class FreezeFrame : MonoBehaviour {
    public static FreezeFrame Instance;
    [SerializeField] private ImpactFrame impactFrame;
    [SerializeField] private bool disableImpactFrames;
    float elapsedTime = 0;
    bool impact = false;
    private void Start() {
        Instance = this;
    }
    public void Freeze(float duration = 0.1f, Transform trans = null) {
        if (disableImpactFrames) return;
        impact = true;
        elapsedTime = duration;
        impactFrame.ActiveImpactFrame();
        impactFrame.UpdateSlashFrame(trans);
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update() {
        if (disableImpactFrames || !impact) return;
        if (elapsedTime > 0) {
            elapsedTime -= Time.unscaledDeltaTime;
        } else {
            Time.timeScale = 1f;
            CameraShaker.instance.StartShake(.15f, .2f);
            impactFrame.InactiveImpactFrame();
            impact = false;
        }
    }
    public void NuclearFrame() {
        impactFrame.Nuclear();
        CameraShaker.instance.StartShake(.3f, .5f);
        NuclearFreeze(.5f);
    }
    private void NuclearFreeze(float duration = 0.1f) {
        if (disableImpactFrames) return;
        impact = true;
        elapsedTime = duration;
        Time.timeScale = 0;
    }
}
