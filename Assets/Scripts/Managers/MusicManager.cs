using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {
    [SerializeField] private AudioSource normalMusic;
    [SerializeField] private AudioSource lowpassMusic;
    [SerializeField] private float transitionSpeed = 5;
    float targetLowPassValue = 3500;
    GameState gameState;
    private void Start() {
        GameState gameState = GameManager.Instance.GetState;

        GameManager.Instance.OnGameStart += ChangeMusicLowPass;
        GameManager.Instance.OnGameRestart += ChangeMusicLowPass;
        GameManager.Instance.OnGameEnd += ChangeMusicLowPass;
    }

    private void ChangeMusicLowPass() {
        gameState = GameManager.Instance.GetState;
    }

    private void Update() {
        if (gameState == GameState.GameOver) {
            normalMusic.volume = Mathf.MoveTowards(normalMusic.volume, 0, transitionSpeed * Time.deltaTime);
            lowpassMusic.volume = Mathf.MoveTowards(lowpassMusic.volume, 1, transitionSpeed * Time.deltaTime);
        } else {
            normalMusic.volume = Mathf.MoveTowards(normalMusic.volume, 1, transitionSpeed * Time.deltaTime);
            lowpassMusic.volume = Mathf.MoveTowards(lowpassMusic.volume, 0, transitionSpeed * Time.deltaTime);
        }
    }
}