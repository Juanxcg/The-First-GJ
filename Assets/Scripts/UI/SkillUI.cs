using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public static SkillUI Instance { get; private set; }
    
    [SerializeField] private Button skillButton;

    public event EventHandler<OnSkillButtonTriggerArgs> OnSkillButtonTrigger;
    public class OnSkillButtonTriggerArgs
    {
        public bool isTrigger;
    }
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        
        skillButton.onClick.AddListener(() => {
            OnSkillButtonTrigger?.Invoke(this, new OnSkillButtonTriggerArgs {
                isTrigger = true
            });
        });
    }
}
