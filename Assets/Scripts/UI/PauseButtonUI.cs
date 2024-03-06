using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonUI : MonoBehaviour
{
    public static PauseButtonUI Instance { get; private set; }
    
    [SerializeField] private Button pauseButton;

    public event EventHandler OnPauseButtonTrigger;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        pauseButton.onClick.AddListener(() => {
            OnPauseButtonTrigger?.Invoke(this, EventArgs.Empty);
        });
    }
}
