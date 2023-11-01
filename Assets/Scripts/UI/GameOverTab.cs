using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverTab : MonoBehaviour
{
    public Button Restart;
    public Button Skins;
    public Button Close;

    public GameObject SkinsTab;

    private void Awake()
    {
        Skins.onClick.AddListener(delegate { OpenTab(SkinsTab); });
        Close.onClick.AddListener(delegate { CloseTab(SkinsTab); });
        Restart.onClick.AddListener(PerformRestart);
    }

    private void OpenTab(GameObject TabToOpen) => TabToOpen.SetActive(true);

    private void CloseTab(GameObject TabToClose) => TabToClose.SetActive(false);

    private void PerformRestart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
