using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreTextMesh;
    private int currentScore;


    void Start()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    public void OnEnable()
    {
        GameEvent.AddScore += AddScore;
    }

    public void OnDisable()
    {
        GameEvent.AddScore -= AddScore;
    }

    private void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreTextMesh.text = currentScore.ToString();
    }
}
