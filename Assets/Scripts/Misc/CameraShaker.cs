using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour {
    public static CameraShaker instance;
    // Start is called before the first frame update
    void Start() {
        if (instance == null) {
            instance = this;
        } else { Destroy(gameObject); }
    }
    public void StartShake(float duration, float magnitude) { StartCoroutine(Shake(duration, magnitude)); }
    IEnumerator Shake(float duration, float magnitude) {
        float elapsed = 0.0f;
        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = Vector3.zero;
    }
}
