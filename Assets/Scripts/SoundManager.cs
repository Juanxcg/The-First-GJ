using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource backgroundAudio;

    private void Awake() {
        backgroundAudio = GetComponent<AudioSource>();
    }

    private void Start() {
        SoundSwitchUI.Instance.OnSoundSwitchButtonTrigger += SoundSwitchUI_OnSoundSwitchButtonTrigger;
    }

    private void
        SoundSwitchUI_OnSoundSwitchButtonTrigger(object sender, SoundSwitchUI.OnSoundSwitchButtonTriggerArg e) {
        if (e.isSoundOn) {
            PlayAudio();
        } else {
            PauseAudio();
        }
    }

    private void PlayAudio() {
        backgroundAudio.Play();
    }

    private void PauseAudio() {
        backgroundAudio.Pause();
    }
}