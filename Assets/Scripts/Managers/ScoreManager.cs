using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private TextMeshProUGUI scoreTextTMP;
    [SerializeField] private Animator scoreAnimator;

    int currentScore = 0;

    void Start() {
        Instance = this;
        GameManager.Instance.OnGameStart += EnableScoreUI;
        GameManager.Instance.OnGameRestart += ResetScore;
    }

    private void EnableScoreUI() {
        scoreUI.gameObject.SetActive(true);
        scoreAnimator.Play("score_squish");
    }

    private void ResetScore() {
        currentScore = 0;
        UpdateScoreText();
    }

    public void AddScore() {
        currentScore++;
        UpdateScoreText();
    }

    private void UpdateScoreText() {
        scoreTextTMP.SetText($"{currentScore}");
        scoreAnimator.Play("score_squish");
    }

    public int GetCurrentScore => currentScore;
}