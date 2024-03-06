using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance { get; private set; }
    
    [SerializeField] private Button playGameButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitGameButton;

    public event EventHandler OnHelpButtonTrigger;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        
        playGameButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
        
        helpButton.onClick.AddListener(() => {
            OnHelpButtonTrigger?.Invoke(this, EventArgs.Empty);
        });
        
        settingsButton.onClick.AddListener(() => {
            // TODO: Show the settings ui
        });
        
        quitGameButton.onClick.AddListener(Application.Quit);
    }
}
