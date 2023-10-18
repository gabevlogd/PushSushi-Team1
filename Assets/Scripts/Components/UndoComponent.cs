using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UndoComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event Action<Vector2Int, UndoComponent> OnStoreMove;
    
    private Grid<Tile> _grid;
    private Vector2Int _lastGridPosition;
    
    private void Awake()
    {
        _grid = new Grid<Tile>(6, 6, 1f, new Vector3(-3f, 0f, -3f), (int x, int y) => new Tile(x, y));
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _lastGridPosition = _grid.GetGridPosition(transform.position);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2Int currentGridPosition = _grid.GetGridPosition(transform.position);
        
        if (HasMoved(_lastGridPosition, currentGridPosition))
            OnStoreMove?.Invoke(_lastGridPosition, this);
    }
    
    private bool HasMoved(Vector2Int previousPosition, Vector2Int currentPosition)
    {
        return previousPosition != currentPosition;
    }
}