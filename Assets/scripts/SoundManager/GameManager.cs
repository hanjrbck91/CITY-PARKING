using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameOver;

    #region OnEnabel/OnDisable
    private void OnEnable()
    {
        SoundManager.OnGameOverSound += PlayGameOverSound;

    }

    private void OnDisable()
    {
        SoundManager.OnGameOverSound -= PlayGameOverSound;
    }
    #endregion
    void PlayGameOverSound()
    {
        gameOver.Play();
    }
}
