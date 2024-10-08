using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UIのスコアテキスト（TextMeshProを使用）
    private int score = 0;

    void Start()
    {
        UpdateScoreText(); // 初期スコアの表示
    }

    // スコアを追加するメソッド
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // スコアテキストを更新するメソッド
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
