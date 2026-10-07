using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance;
    [SerializeField] private TextMeshProUGUI scoreTextTMP;

    int currentScore = 0;

    void Start() {
        Instance = this;
    }

    public void AddScore() {
        currentScore++;
        UpdateScoreText();
    }

    private void UpdateScoreText() {
        scoreTextTMP.SetText($"{currentScore}");
    }

    public int GetCurrentScore => currentScore;
}