using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(Hide);
        
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        
        PauseButtonUI.Instance.OnPauseButtonTrigger += PauseButtonUI_OnPauseButtonTrigger;
        
        Hide();
    }

    private void PauseButtonUI_OnPauseButtonTrigger(object sender, EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
