using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDData : MonoBehaviour
{
    public TextMeshProUGUI MoveCounter;
    public TextMeshProUGUI LevelCounter;
    public TextMeshProUGUI BestMoves;
    public TextMeshProUGUI Difficulty;

    public Image Score;
    public Sprite[] ScoreSprites;

    public Button Pause;
    public Button Skins;
    public Button Stages;
    public Button Resume;
    public Button Restart;
    public Button Close;

    public GameObject PauseTab;
    public GameObject SkinsTab;
    public GameObject GameOverTab;
}
