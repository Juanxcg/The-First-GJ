using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChessCaptureNumberUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;

    private int capturedNumber = 0;

    private void Start() {
        Board.Instance.OnCapturedChess += Board_OnCapturedChess;
        
        UpdateVisual();
    }

    private void Board_OnCapturedChess(object sender, EventArgs e) {
        IncreaseCapturedNumber();
    }

    private void IncreaseCapturedNumber() {
        capturedNumber++;
        
        UpdateVisual();
    }

    private void UpdateVisual() {
        numberText.text = capturedNumber.ToString();
    }
}
