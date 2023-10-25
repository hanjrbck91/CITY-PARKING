using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlaySoundController : MonoBehaviour
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
        SoundManager.OnButtonClickSound += PlayButtonClickSound;
        SoundManager.OnMuteButtonClick += MuteVolume;
        SoundManager.OnBackgroundVolumeChange += UpdateBackgroundVolumeSlider;
        SoundManager.OnSFXVolumeChange += UpdateSFXVolumeSlider;
    }

    private void OnDisable()
    {
        SoundManager.OnButtonClickSound -= PlayButtonClickSound;
        SoundManager.OnMuteButtonClick -= MuteVolume;
        SoundManager.OnBackgroundVolumeChange -= UpdateBackgroundVolumeSlider;
        SoundManager.OnSFXVolumeChange -= UpdateSFXVolumeSlider;
    }
    #endregion

    #region Methods
    private void PlayButtonClickSound()
    {
        buttonSound.Play();
    }

    private void MuteVolume()
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

    private void UpdateBackgroundVolumeSlider()
    {
        // Update the Background Volume Slider value when the volume changes
        AudioListener.volume = backgroundVolumeSlider.value;
    }

    private void UpdateSFXVolumeSlider()
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
