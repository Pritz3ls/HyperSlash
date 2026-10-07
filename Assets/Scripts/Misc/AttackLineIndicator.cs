using UnityEngine;

public class AttackLineIndicator : MonoBehaviour {
    [SerializeField] private LineRenderer lineRenderer;

    public void SetAttackLine(Vector2 origin, Vector2 target) {
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, target);
    }
    public void ResetAttackLine() {
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, Vector2.zero);
    }
}