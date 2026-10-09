using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {
    public static SFXManager Instance;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] sfx;
    /*
        0 - player swing
        1 - player death
        2 - ninja swing
        3 - ninja death
        4 - ninja death alt
        5 - nuke/restart
    */

    // Start is called before the first frame update
    void Start() {
        Instance = this;
    }

    public void PlaySFX(int index) {
        if (index < 0 || index >= sfx.Length) return;
        sfxSource.PlayOneShot(sfx[index]);
    }

    public void PlayNinjaDeath() {
        int random = Random.Range(3, 5);
        sfxSource.PlayOneShot(sfx[random]);
    }
}
