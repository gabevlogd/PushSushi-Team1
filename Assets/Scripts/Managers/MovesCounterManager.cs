using TMPro;
using UnityEngine;

public class MovesCounterManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI movesText;
    private int movesCount = 0;

    //private void OnEnable() => SlidableComponent.OnLevelComplete += IncreaseMovesCount;

    //private void OnDisable() => SlidableComponent.OnLevelComplete -= IncreaseMovesCount;

    public void IncreaseMovesCount()
    {
        movesCount++;
        UpdateMovesText();
    }

    public void DecreaseMovesCount()
    {
        if (movesCount > 0)
        {
            movesCount--;
            UpdateMovesText();
        }
    }

    private void UpdateMovesText()
    {
        movesText.text = movesCount.ToString();
    }
}