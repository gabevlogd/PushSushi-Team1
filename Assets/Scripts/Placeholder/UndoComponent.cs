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
        //store the grid's coordinate before starting to move the slidable component
        _grid.GetXY(transform.position, out int x, out int y);
        _lastGridPosition = new Vector2Int(x, y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _grid.GetXY(transform.position, out int x, out int y);
        Vector2Int currentGridPosition = new Vector2Int(x, y);

        if (_lastGridPosition != currentGridPosition)
            OnStoreMove?.Invoke(_lastGridPosition, this);
    }
}
