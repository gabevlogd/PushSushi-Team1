using TMPro;
using UnityEngine;

public class MovesCounterManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI movesText;
    private int movesCount = 0;

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