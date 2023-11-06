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

    public delegate void SoundEvent();
    public static SoundEvent ButtonSound;
    public static SoundEvent CoinSound;
    public static SoundEvent PawnSound;
    public static SoundEvent GameOverSound;

    private void Awake()
    {

        if (_instance == null)
            _instance = this;
        else 
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);

        ButtonSound += () => PlaySound(_buttonClick);
        CoinSound += () => PlaySound(_coinPickUp);
        PawnSound += () => PlaySound(_pawnSlide);
        GameOverSound += () => PlaySound(_gameOver);
    }


    private void PlaySound(AudioSource audioSource)
    {
        if (audioSource.enabled)
            audioSource.Play();
    }

    private void ToggleSoundSource(AudioSource audioSource)
    {
        audioSource.enabled = !audioSource.enabled;
    }
}
