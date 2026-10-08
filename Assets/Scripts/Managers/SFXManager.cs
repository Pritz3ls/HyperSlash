using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {
    public static SFXManager Instance;
    // Start is called before the first frame update
    void Start() {
        Instance = this;
    }
}
