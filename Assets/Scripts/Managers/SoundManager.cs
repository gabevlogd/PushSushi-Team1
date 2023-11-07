using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    [SerializeField]
    private AudioSource _buttonClick;
    [SerializeField]
    private AudioSource _coinPickUp;
    [SerializeField]
    private AudioSource _pawnSlide;
    [SerializeField]
    private AudioSource _gameOver;
    [SerializeField]
    private AudioSource _music;

    public delegate void SoundEvent();
    public static SoundEvent ButtonSound;
    public static SoundEvent CoinSound;
    public static SoundEvent PawnSound;
    public static SoundEvent GameOverSound;

    public static bool _musicOn = true;
    public static bool _soundOn = true;

    private void Awake()
    {

        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        ButtonSound += () => PlaySound(_buttonClick);
        CoinSound += () => PlaySound(_coinPickUp);
        PawnSound += () => PlaySound(_pawnSlide);
        GameOverSound += () => PlaySound(_gameOver);

        OptionMenuUI.OnToggleSound += ToggleSoundSources;
        OptionMenuUI.OnToggleMusic += ToggleMusicSource;
        HUDManager.OnToggleSound += ToggleSoundSources;
        HUDManager.OnToggleMusic += ToggleMusicSource;
    }


    private void PlaySound(AudioSource audioSource)
    {
        if (audioSource.enabled)
            audioSource.Play();
    }

    private void ToggleSoundSources(bool value)
    {
        ToggleAudioSource(_gameOver, value);
        ToggleAudioSource(_pawnSlide, value);
        ToggleAudioSource(_coinPickUp, value);
        ToggleAudioSource(_buttonClick, value);
        if (value)
            ButtonSound?.Invoke();
        _soundOn = value;
    }

    private void ToggleMusicSource(bool value)
    {
        ButtonSound?.Invoke();
        ToggleAudioSource(_music, value);
        _musicOn = value;
    }

    private void ToggleAudioSource(AudioSource audioSource, bool value) => audioSource.enabled = value;
}
