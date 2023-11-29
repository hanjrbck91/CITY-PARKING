using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Events/Actions
    public static event Action OnButtonClickSound;
    public static event Action OnGameOverSound;
    public static event Action<string> OnCarSound;
    public static event Action OnMuteButtonClick;

    public static event Action OnBackgroundVolumeChange;
    public static event Action OnSFXVolumeChange;

    #endregion

    #region Volume Slider Management 
    //Define variables for background and sfx volume levels
    private float backgroundVolume = 1.0f;
    private float sfxVolume = 1.0f; 

    //Getter method to acess volume levels
    public float GetSFXVolume() => sfxVolume;
    public float GetBackgroundVolume() => backgroundVolume;

    #endregion

    #region InvokeEvent
    public void ButtonClickSound() => OnButtonClickSound?.Invoke();
    public void GameOverSound() => OnGameOverSound?.Invoke();
    public void CarSound(string carType) => OnCarSound?.Invoke(carType);
    public void MuteButtonOnClick() => OnMuteButtonClick?.Invoke();
    public void BackgroundVolumeChange() => OnBackgroundVolumeChange?.Invoke();
    public void SFXVolumeChange() => OnSFXVolumeChange?.Invoke();
    #endregion

}
