using UnityEngine;

public class ImpactFrame : MonoBehaviour {
    [SerializeField] private GameObject blackBackground;
    public void ActiveImpactFrame() {
        blackBackground.SetActive(true);
    }
    public void InactiveImpactFrame() {
        blackBackground.SetActive(false);
    }
}