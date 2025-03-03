using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    private int score;

    void Start() {
        CoinScript[] coins = FindObjectsByType<CoinScript>(FindObjectsSortMode.None);
        foreach (CoinScript coin in coins) {
            Debug.Log("" + coin.name);
            coin.CoinCollected.AddListener(IncrementScore);
        }
    }

    private void IncrementScore() {
        score++;
        scoreText.text = $"Score: {score:00}";
    }
}
