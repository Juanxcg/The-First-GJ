using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButtonUI : MonoBehaviour
{
    public static HelpButtonUI Instance { get; private set; }
    
    [SerializeField] private Button helpButton;

    public event EventHandler OnHelpButtonTrigger;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        helpButton.onClick.AddListener(() => {
            OnHelpButtonTrigger?.Invoke(this, EventArgs.Empty);
        });
    }
}
