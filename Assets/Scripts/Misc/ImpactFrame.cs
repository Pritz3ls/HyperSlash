using UnityEngine;

public class ImpactFrame : MonoBehaviour {
    [SerializeField] private GameObject blackBackground;
    [SerializeField] private GameObject whiteBackground;
    [SerializeField] private Transform slashFrame;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float rotationOffset = 0f;
    private void Start() {
        playerController = GameObject.FindAnyObjectByType<PlayerController>();
    }
    public void ActiveImpactFrame() {
        blackBackground.SetActive(true);
    }

    public void InactiveImpactFrame() {
        blackBackground.SetActive(false);
        whiteBackground.SetActive(false);
    }

    public void UpdateSlashFrame(Transform trans) {
        if (playerController == null || trans == null) return;

        Vector2 direction = (Vector2)trans.position - (Vector2)playerController.GetLastPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        slashFrame.position = playerController.transform.position;
        // Apply the offset to correct default sprite orientation
        slashFrame.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }
    public void Nuclear() {
        slashFrame.rotation = Quaternion.identity;
        slashFrame.transform.position = Vector2.up * 50;
        whiteBackground.SetActive(true);
    }
}