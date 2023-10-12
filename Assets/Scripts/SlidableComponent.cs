using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SlidableComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SlidingDirection SlidingDirection;
    public Transform SensorA;
    public Transform SensorB;

    private Camera _camera;

    private Vector3 _offSet;
    private Vector3 _offSetA;
    private Vector3 _offSetB;


    private Vector3 _limiterA;
    private Vector3 _limiterB;

    private Grid<Tile> _grid;

    private bool _grabbed;
    private bool _canGoUp, _canGoDown, _canGoLeft, _canGoRight;


    private void Awake()
    {
        _camera = Camera.main;
        _grid = new Grid<Tile>(6, 6, 1f, new Vector3(-3, -3, 0f), (int x, int y) => new Tile(x, y));
    }

    private void Update()
    {
        CalculateAllowedDirections();
        if (_grabbed) CollisionHandler(_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(transform.position, _camera.transform.position))));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CalculateSlidableAreaLimits();
        Vector3 pointerPosition = eventData.pointerCurrentRaycast.worldPosition;

        //calculate the offset between the clicked point and the transform of the slidable component 
        _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;

        _offSetA = new Vector3(SensorA.position.x - 0.01f, transform.position.y, SensorA.position.z - 0.01f) - transform.position;
        _offSetB = new Vector3(SensorB.position.x + 0.01f, transform.position.y, SensorB.position.z + 0.01f) - transform.position;

        _grabbed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _grabbed = false;
        //when release the slidable component makes sure the transform remain on a fixed position of the grid
        _grid.GetXY(transform.position, out int x, out int y);
        transform.position = new Vector3(_grid.GetWorldPosition(x, y).x, transform.position.y, _grid.GetWorldPosition(x, y).z);
    }

    /// <summary>
    /// For debug
    /// </summary>
    private void OnDrawGizmos()
    {
        Debug.DrawLine(_limiterA, _limiterB);
    }

    /// <summary>
    /// Calculates the allowed directions where the slidable component can slides to
    /// </summary>
    private void CalculateAllowedDirections()
    {
        _canGoLeft = Mathf.Abs(transform.position.x - (_limiterA.x - _offSetA.x)) > 0.1f;
        _canGoRight = Mathf.Abs(transform.position.x - (_limiterB.x - _offSetB.x)) > 0.1f;
        _canGoDown = Mathf.Abs(transform.position.z - (_limiterA.z - _offSetA.z)) > 0.1f;
        _canGoUp = Mathf.Abs(transform.position.z - (_limiterB.z - _offSetB.z)) > 0.1f;
    }

    /// <summary>
    /// Slides horizontally the slidable component only in the alowed directions
    /// </summary>
    /// <param name="pointerPosition"></param>
    private void SlideHorizontal(Vector3 pointerPosition)
    {
        if (_canGoRight && _canGoLeft)
            transform.position = new Vector3(pointerPosition.x - _offSet.x, transform.position.y, transform.position.z);
        else if (!_canGoLeft)
        {
            if (pointerPosition.x - _offSet.x > _limiterA.x - _offSetA.x) 
                transform.position = new Vector3(pointerPosition.x - _offSet.x, transform.position.y, transform.position.z);
        }
        else if (!_canGoRight)
        {
            if (pointerPosition.x - _offSet.x < _limiterB.x - _offSetB.x)
                transform.position = new Vector3(pointerPosition.x - _offSet.x, transform.position.y, transform.position.z);
        }
    }

    /// <summary>
    /// Slides vertically the slidable component only in the alowed directions
    /// </summary>
    /// <param name="pointerPosition"></param>
    private void SlideVertical(Vector3 pointerPosition)
    {
        if (_canGoDown && _canGoUp)
            transform.position = new Vector3(transform.position.x, transform.position.y, pointerPosition.z - _offSet.z);
        else if (!_canGoDown)
        {
            if (pointerPosition.z - _offSet.z > _limiterA.z - _offSetA.z)
                transform.position = new Vector3(transform.position.x, transform.position.y, pointerPosition.z - _offSet.z);
        }
        else if (!_canGoUp)
        {
            if (pointerPosition.z - _offSet.z < _limiterB.z - _offSetB.z)
                transform.position = new Vector3(transform.position.x, transform.position.y, pointerPosition.z - _offSet.z);
        }
    }

    /// <summary>
    /// Calculates the max and min position where the slidable component can moves between
    /// </summary>
    private void CalculateSlidableAreaLimits()
    {
        RaycastHit raycastHitA;
        RaycastHit raycastHitB;
        Ray rayA = new Ray(transform.position + Vector3.up * 0.5f, SensorA.forward);
        Ray rayB = new Ray(transform.position + Vector3.up * 0.5f, SensorB.forward);
        if (Physics.Raycast(rayA, out raycastHitA)) _limiterA = raycastHitA.point;
        else Debug.Log("Level Complete");
        if (Physics.Raycast(rayB, out raycastHitB)) _limiterB = raycastHitB.point;
        else Debug.Log("Level Complete");
        //Debug.Log(limiterA);
        //Debug.Log(limiterB);
    }

    /// <summary>
    /// Simulates the collisions by checking the slidable component position
    /// </summary>
    /// <param name="pointerPosition"></param>
    private void CollisionHandler(Vector3 pointerPosition)
    {
        if (!_canGoUp && !_canGoDown && !_canGoLeft && !_canGoRight) return;

        if (SlidingDirection == SlidingDirection.Horizontal)
        {
            if (SensorA.position.x > _limiterA.x && SensorB.position.x < _limiterB.x)
                SlideHorizontal(pointerPosition);
            else if (SensorA.position.x <= _limiterA.x)
            {
                transform.position = new Vector3(_limiterA.x - _offSetA.x, transform.position.y, transform.position.z);
                _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;
            }
            else if (SensorB.position.x >= _limiterB.x)
            {
                transform.position = new Vector3(_limiterB.x - _offSetB.x, transform.position.y, transform.position.z);
                _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;
            }
        }
        else
        {
            if (SensorA.position.z > _limiterA.z && SensorB.position.z < _limiterB.z)
                SlideVertical(pointerPosition);
            else if (SensorA.position.z <= _limiterA.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, _limiterA.z - _offSetA.z);
                _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;
            }
            else if (SensorB.position.z >= _limiterB.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, _limiterB.z - _offSetB.z);
                _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;
            }
        }
    }
}

public enum SlidingDirection
{
    Vertical,
    Horizontal
}
