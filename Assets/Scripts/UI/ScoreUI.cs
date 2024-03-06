using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Start() {
        SearchPieces.OnAnyScoreIncrease += SearchPieces_OnAnyScoreIncrease;
        
        UpdateVisual();
    }

    private void SearchPieces_OnAnyScoreIncrease(object sender, SearchPieces.OnAnyScoreIncreaseArgs e) {
        IncreaseScore(e.increasedScore);
    }

    private void IncreaseScore(int increasement) {
        score += increasement;
        
        UpdateVisual();
    }

    private void UpdateVisual() {
        scoreText.text = score.ToString();
    }
}
