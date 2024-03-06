using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    
    
    private void Start() {
        if (MainMenuUI.Instance != null) {
            MainMenuUI.Instance.OnHelpButtonTrigger += MainMenuUI_OnHelpButtonTrigger;
        }
        if (HelpButtonUI.Instance != null) {
            HelpButtonUI.Instance.OnHelpButtonTrigger += HelpButtonUI_OnHelpButtonTrigger;
        }
        
        resumeButton.onClick.AddListener(Hide);
        
        Hide();
    }

    private void HelpButtonUI_OnHelpButtonTrigger(object sender, EventArgs e) {
        Show();
    }

    private void MainMenuUI_OnHelpButtonTrigger(object sender, EventArgs e) {
        Show();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}
