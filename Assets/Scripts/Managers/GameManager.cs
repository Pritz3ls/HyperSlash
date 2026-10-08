using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    [SerializeField] private Player player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameState gameState = GameState.Idle;
    [SerializeField] private GameObject pressStartTextGameObject;
    [SerializeField] private GameObject titleElementsObject;
    [SerializeField] private TextMeshProUGUI pressStartTMP;
    [SerializeField] private float delayRestartDuration;

    float elapsedTimeToRestart = 0;
    public event Action OnGameStart;
    public event Action OnGameRestart;
    public event Action OnGameEnd;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        player.OnDeath += GameOver;
    }

    private void Update() {
        if (gameState == GameState.Ingame) return;

        if (Input.GetMouseButtonDown(0)) {
            if (gameState == GameState.GameOver) {
                if (elapsedTimeToRestart <= 0) {
                    RestartGame();
                }
            } else {
                StartGame();
            }
        }
        if (elapsedTimeToRestart > 0) {
            elapsedTimeToRestart -= Time.unscaledDeltaTime;
        }
    }

    private void StartGame() {
        gameState = GameState.Ingame;
        OnGameStart?.Invoke();
        HideRestartText();
        titleElementsObject.SetActive(false);
    }
    private void RestartGame() {
        gameState = GameState.Ingame;
        OnGameRestart?.Invoke();

        HideRestartText();
        StartCoroutine(DelayEnablePlayer());
    }

    private void GameOver() {
        pressStartTMP.SetText("Click anywhere to restart");
        pressStartTextGameObject.SetActive(true);

        gameState = GameState.GameOver;
        OnGameEnd?.Invoke();
        elapsedTimeToRestart = delayRestartDuration;
    }

    private void HideRestartText() {
        pressStartTextGameObject.SetActive(false);
    }

    IEnumerator DelayEnablePlayer() {
        yield return new WaitForSecondsRealtime(delayRestartDuration - 1);
        playerController.gameObject.SetActive(true);
    }

    public bool IsGameOver => gameState == GameState.GameOver;
    public Player GetPlayer => player;
}
public enum GameState {
    Idle, Ingame, GameOver
}