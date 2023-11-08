using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSound : MonoBehaviour
{
    private void PlaySound() => SoundManager.GameOverSound?.Invoke();
}
