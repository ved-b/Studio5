using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TMP_Text scoreText;
    private int score = 0;
    private Vector2 originalPos;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        if (scoreText == null)
            scoreText = GetComponent<TMP_Text>();

        originalPos = scoreText.GetComponent<RectTransform>().anchoredPosition;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
        AnimateScore();
    }

    void AnimateScore() {
        scoreText.transform.DOKill();
        RectTransform rt = scoreText.GetComponent<RectTransform>();
        rt.DOKill();

        scoreText.transform.DOScale(1.5f, 0.2f).OnComplete(() => {
            scoreText.transform.DOScale(1f, 0.2f);
        });

        rt.anchoredPosition = originalPos + new Vector2(0, 50);
        rt.DOAnchorPos(originalPos, 0.5f).SetEase(Ease.OutCubic);
    }
}