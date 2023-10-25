using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour
{
    #region Fields
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource backgroundSound;
    [SerializeField] private Image muteIcon;
    [SerializeField] private Image unmuteIcon;
    [SerializeField] Slider backgroundVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    #endregion

    #region OnEnabel/OnDisable
    private void OnEnable()
    {
        SoundManager.OnButtonClickSound += PlayButtonClickSoundUI;
        SoundManager.OnMuteButtonClick += MuteVolumeUI;
        SoundManager.OnBackgroundVolumeChange += UpdateBackgroundVolumeSliderUI;
        SoundManager.OnSFXVolumeChange += UpdateSFXVolumeSliderUI;
    }

    private void OnDisable()
    {
        SoundManager.OnButtonClickSound -= PlayButtonClickSoundUI;
        SoundManager.OnMuteButtonClick -= MuteVolumeUI;
        SoundManager.OnBackgroundVolumeChange -= UpdateBackgroundVolumeSliderUI;
        SoundManager.OnSFXVolumeChange -= UpdateSFXVolumeSliderUI;
    }
    #endregion

    #region Methods
    private void PlayButtonClickSoundUI()
    {
            buttonSound.Play();
    }

    private void PlayCarEngineUISoundUI()
    {

    }

    private void MuteVolumeUI()
    {
        if (backgroundSound != null)
        {
            // Toggle mute/unmute by setting the volume to 0 or 1
            if (backgroundSound.volume > 0)
            {
                backgroundSound.volume = 0; // Mute
                muteIcon.gameObject.SetActive(true); // Show the mute icon
                unmuteIcon.gameObject.SetActive(false); // Hide the unmute icon
            }
            else
            {
                backgroundSound.volume = .1f; // Unmute
                muteIcon.gameObject.SetActive(false); // Hide the mute icon
                unmuteIcon.gameObject.SetActive(true); // Show the unmute icon
            }
        }
    }

     private void UpdateBackgroundVolumeSliderUI()
    {
        // Update the Background Volume Slider value when the volume changes
         AudioListener.volume = backgroundVolumeSlider.value; 
    }

    private void UpdateSFXVolumeSliderUI()
    {
        // Update the SFX volume (button click sound) based on the slider value
        float sfxVolume = sfxVolumeSlider.value;

        // Make sure the volume is clamped between 0 and 1
        sfxVolume = Mathf.Clamp01(sfxVolume);

        // Set the volume of your button click sound (SFX sound)
        buttonSound.volume = sfxVolume;
    }

    #endregion
}
