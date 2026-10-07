using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrame : MonoBehaviour {
    public static FreezeFrame Instance;
    float elapsedTime = 0;
    private void Start() {
        Instance = this;
    }
    public void Freeze(float duration = 0.1f) {
        elapsedTime = duration;
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update() {
        if (elapsedTime > 0) {
            elapsedTime -= Time.unscaledDeltaTime;
        } else {
            Time.timeScale = 1f;
        }
    }
}
