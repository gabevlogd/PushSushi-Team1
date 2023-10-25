using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class SlidableComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform SensorA;
    public Transform SensorB;
    private Camera _camera;
    private Grid<Tile> _grid;

    private event Action<Vector3> OnPerformMovement;
    private event Action OnPerformAllowedDirections;

    private Vector3 _offSet;
    private Vector3 _offSetA;
    private Vector3 _offSetB;
    private Vector3 _limiterA;
    private Vector3 _limiterB;

    private bool _grabbed;
    private bool _levelComplete;
    private bool _canGoUp, _canGoDown, _canGoLeft, _canGoRight;
    private float _speed = 20f;


    private void Awake()
    {
        _camera = Camera.main;
        _grid = new Grid<Tile>(6, 6, 1f, new Vector3(-3f, 0f, -3f), (int x, int y) => new Tile(x, y));

        if (transform.right == Vector3.forward)
        {
            OnPerformMovement += PerformVerticalMovement;
            OnPerformAllowedDirections += CalculateAllowedVerticalDirections;
        }
        else
        {
            OnPerformMovement += PerformHorizontalMovement;
            OnPerformAllowedDirections += CalculateAllowedHorizontalDirections;
        }
    }

    private void Update()
    {
        if (_levelComplete)
            SlideAway();
        if (_grabbed)
        {
            OnPerformAllowedDirections();
            OnPerformMovement(GetPointerWorldPosition());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.touchCount > 1) return;

        CalculateSlidableAreaLimits();
        Vector3 pointerPosition = GetPointerWorldPosition();

        //calculate the offset between the clicked point and the transform of the slidable component 
        _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;

        //calculate the offsets between the collisions points and the transform of the slidable component
        _offSetA = new Vector3(SensorA.position.x , transform.position.y, SensorA.position.z ) - transform.position;
        _offSetB = new Vector3(SensorB.position.x , transform.position.y, SensorB.position.z ) - transform.position;

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
    /// Calculates the allowed vertical directions where the slidable component can slides to
    /// </summary>
    private void CalculateAllowedVerticalDirections()
    {
        _canGoDown = SensorA.position.z > _limiterA.z;
        _canGoUp = SensorB.position.z < _limiterB.z;
    }

    /// <summary>
    /// Calculates the allowed horizontal directions where the slidable component can slides to
    /// </summary>
    private void CalculateAllowedHorizontalDirections()
    {
        _canGoLeft = SensorA.position.x > _limiterA.x;
        _canGoRight = SensorB.position.x < _limiterB.x;
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
        else
        {
            _levelComplete = true;
            StartCoroutine(GoToNextLevel()); //only for second build
        }

        if (Physics.Raycast(rayB, out raycastHitB)) _limiterB = raycastHitB.point;
        else
        {
            _levelComplete = true;
            StartCoroutine(GoToNextLevel()); //only for second build
        }
    }

    private void PerformVerticalMovement(Vector3 pointerPosition)
    {
        if (!_canGoUp && !_canGoDown) return;
        float z = Math.Clamp(pointerPosition.z - _offSet.z, _limiterA.z - _offSetA.z, _limiterB.z - _offSetB.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    private void PerformHorizontalMovement(Vector3 pointerPosition)
    {
        if (!_canGoLeft && !_canGoRight) return;
        float x = Math.Clamp(pointerPosition.x - _offSet.x, _limiterA.x - _offSetA.x, _limiterB.x - _offSetB.x);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void SlideAway() => transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right * 20f, Time.deltaTime * _speed);

    private Vector3 GetPointerWorldPosition() => _camera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Vector3.Distance(transform.position, _camera.transform.position)));

    #region PLACEHOLDER FOR SECOND BUILD
    public static event Action OnLevelComplete;
    private static event Action OnDisableComponent;

    private IEnumerator GoToNextLevel()
    {
        OnDisableComponent?.Invoke();
        yield return new WaitForSeconds(2f);
        OnLevelComplete?.Invoke();
    }

    private void DisableComponent() => GetComponent<BoxCollider>().enabled = false;

    private void OnEnable() => OnDisableComponent += DisableComponent;
    private void OnDisable() => OnDisableComponent -= DisableComponent;

    #endregion

}

public enum SlidingDirection
{
    Vertical,
    Horizontal
}









//LE TENGO QUI PER RICORDARMI DI QUANTO IO SIA STUPIDO A VOLTE :) 


//private void PerformVerticalMovement(Vector3 pointerPosition)
//{
//if (!_canGoUp && !_canGoDown) return;


//if (_canGoUp && _canGoDown)
//transform.position = new Vector3(transform.position.x, transform.position.y, pointerPosition.z - _offSet.z);
//else if (!_canGoUp)
//{
//    transform.position = new Vector3(transform.position.x, transform.position.y, _limiterB.z - _offSetB.z);

//    if (pointerPosition.z - _offSet.z < _limiterB.z - _offSetB.z)
//        transform.position = new Vector3(transform.position.x, transform.position.y, pointerPosition.z - _offSet.z);

//    _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;
//}
//else if (!_canGoDown)
//{
//    transform.position = new Vector3(transform.position.x, transform.position.y, _limiterA.z - _offSetA.z);

//    if (pointerPosition.z - _offSet.z > _limiterA.z - _offSetA.z)
//        transform.position = new Vector3(transform.position.x, transform.position.y, pointerPosition.z - _offSet.z);

//    _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;
//}
//}

//private void PerformHorizontalMovement(Vector3 pointerPosition)
//{
//if (!_canGoLeft && !_canGoRight) return;

//if (_canGoLeft && _canGoRight)
//transform.position = new Vector3(pointerPosition.x - _offSet.x, transform.position.y, transform.position.z);
//else if (!_canGoLeft)
//{
//    transform.position = new Vector3(_limiterA.x - _offSetA.x, transform.position.y, transform.position.z);

//    if (pointerPosition.x - _offSet.x > _limiterA.x - _offSetA.x)
//        transform.position = new Vector3(pointerPosition.x - _offSet.x, transform.position.y, transform.position.z);

//    _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;
//}
//else if (!_canGoRight)
//{
//    transform.position = new Vector3(_limiterB.x - _offSetB.x, transform.position.y, transform.position.z);

//    if (pointerPosition.x - _offSet.x < _limiterB.x - _offSetB.x)
//        transform.position = new Vector3(pointerPosition.x - _offSet.x, transform.position.y, transform.position.z);

//    _offSet = new Vector3(pointerPosition.x, transform.position.y, pointerPosition.z) - transform.position;
//}

//}

/// <summary>
/// Calculates the allowed vertical directions where the slidable component can slides to
/// </summary>
//private void CalculateAllowedVerticalDirections()
//{
//    _canGoDown = SensorA.position.z > _limiterA.z;
//    _canGoUp = SensorB.position.z < _limiterB.z;
//}

/// <summary>
/// Calculates the allowed horizontal directions where the slidable component can slides to
/// </summary>
//private void CalculateAllowedHorizontalDirections()
//{
//    _canGoLeft = SensorA.position.x > _limiterA.x;
//    _canGoRight = SensorB.position.x < _limiterB.x;
//}
