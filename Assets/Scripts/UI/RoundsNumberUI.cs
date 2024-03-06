using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundsNumberUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;

    private int roundsNumber = 1;

    private void Start() {
        Move.Instance.OnCurrentRoundEnd += Move_OnCurrentRoundEnd;
        PutDown.Instance.OnCurrentRoundEnd += PutDown_OnCurrentRoundEnd;
        
        UpdateVisual();
    }

    private void PutDown_OnCurrentRoundEnd(object sender, EventArgs e) {
        IncreaseRoundsNumber();
    }

    private void Move_OnCurrentRoundEnd(object sender, EventArgs e) {
        IncreaseRoundsNumber();
    }

    private void IncreaseRoundsNumber() {
        roundsNumber++;
        UpdateVisual();
    }

    private void UpdateVisual() {
        numberText.text = $"当前回合数: {roundsNumber}";
    }
}