using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundSwitchUI : MonoBehaviour
{
    public static SoundSwitchUI Instance { get; private set; }
    
    [SerializeField] private Button switchButton;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    public event EventHandler<OnSoundSwitchButtonTriggerArg> OnSoundSwitchButtonTrigger;
    public class OnSoundSwitchButtonTriggerArg
    {
        public bool isSoundOn;
    }
    
    private bool isSoundOn = true;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        switchButton.onClick.AddListener(() => {
            isSoundOn = !isSoundOn;
            
            ChangeButtonImage(isSoundOn);
            
            OnSoundSwitchButtonTrigger?.Invoke(this, new OnSoundSwitchButtonTriggerArg {
                isSoundOn = isSoundOn
            });
        });
    }

    private void ChangeButtonImage(bool isSoundOn) {
        if (isSoundOn) {
            switchButton.GetComponent<Image>().sprite = soundOnSprite;
        } else {
            switchButton.GetComponent<Image>().sprite = soundOffSprite;
        }
    }
}
