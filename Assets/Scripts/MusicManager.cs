using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource chessFallingAudio;

    private void Awake() {
        chessFallingAudio = GetComponent<AudioSource>();
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
        chessFallingAudio.Play();
    }

    private void PauseAudio() {
        chessFallingAudio.Pause();
    }
}
