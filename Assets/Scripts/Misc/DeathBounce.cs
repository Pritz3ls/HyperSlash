using System.Collections;
using UnityEngine;

public class DeathBounce : MonoBehaviour {
    [SerializeField] private float bounceForce = 8f;
    [SerializeField] private float fakeHeight = 2f;
    [SerializeField] private float bounceDuration = 0.8f;
    [SerializeField] private Transform visualTransform;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private ParticleSystem bounceParticle;

    private void Awake() {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (visualTransform == null) visualTransform = transform;
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void OnDeath(Vector2 hitSourcePosition) {
        spriteRenderer.gameObject.SetActive(true);
        transform.position = playerTransform.position;
        Vector2 hitDirection = ((Vector2)transform.position - hitSourcePosition).normalized;
        hitDirection = -hitDirection;

        if (spriteRenderer != null) {
            spriteRenderer.flipX = hitDirection.x > 0;
        }

        rb.isKinematic = false;
        rb.velocity = -hitDirection * bounceForce;

        StartCoroutine(BounceRoutine());
    }

    private IEnumerator BounceRoutine() {
        float elapsed = 0f;
        Vector3 initialLocalPos = visualTransform.localPosition;
        int lastBounceIndex = -1;

        while (elapsed < bounceDuration) {
            elapsed += Time.deltaTime;
            float t = elapsed / bounceDuration;

            float rawSin = Mathf.Sin(t * Mathf.PI * 2f);
            float height = Mathf.Abs(rawSin) * fakeHeight * (1f - t);
            visualTransform.localPosition = initialLocalPos + new Vector3(0f, height, 0f);

            int currentBounceIndex = (t < 0.5f) ? 0 : 1;
            if (rawSin <= 0.05f && currentBounceIndex != lastBounceIndex) {
                lastBounceIndex = currentBounceIndex;
                TriggerBounceParticle();
            }

            yield return null;
        }

        TriggerBounceParticle();

        visualTransform.localPosition = initialLocalPos;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    private void TriggerBounceParticle() {
        if (bounceParticle != null) {
            bounceParticle.transform.position = transform.position;
            bounceParticle.Play();
        }
    }

    public void ResetCorpse() {
        spriteRenderer.gameObject.SetActive(false);
    }
}