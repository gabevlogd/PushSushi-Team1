using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 _pointerDownPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDownPosition = transform.position;  // Salva la posizione iniziale al PointerDown
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3 pointerUpPosition = transform.position;  // Ottiene la posizione al PointerUp

        if (_pointerDownPosition != pointerUpPosition)  // Compara le posizioni
        {
            // Il blocco è stato mosso
            Vector2Int startPosition = new Vector2Int(Mathf.RoundToInt(_pointerDownPosition.x), Mathf.RoundToInt(_pointerDownPosition.z));
            Vector2Int endPosition = new Vector2Int(Mathf.RoundToInt(pointerUpPosition.x), Mathf.RoundToInt(pointerUpPosition.z));
            GameManager.Instance.OnBlockMoved(GetComponent<Block>(), startPosition, endPosition);
            Debug.Log("Start: "+ startPosition);
            Debug.Log("End: "+ endPosition);
        }
    }
}