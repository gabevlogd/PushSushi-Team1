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
    private Animator _animator;

    private event Action<Vector3> OnPerformMovement;
    private event Action OnPerformAllowedDirections;
    public static event Action OnLevelComplete;

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
        _animator = GetComponentInChildren<Animator>();
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
            SlideTo(transform.position + Vector3.right * 20f);

        if (_grabbed && LevelManager.GameState != GameState.GameOver)
        {
            OnPerformAllowedDirections();
            OnPerformMovement(GetPointerWorldPosition());
            _animator?.Play("OpenEyes");
        }
        else _animator?.Play("CloseEyes");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (LevelManager.GameState == GameState.Tutorial && TutorialManager.TargetPawns.Count > 0 && this != TutorialManager.TargetPawns[0]) return;
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

        if (LevelManager.GameState == GameState.Tutorial && TutorialManager.TargetPawns.Count > 0 && this == TutorialManager.TargetPawns[0])
        {
            if (y == 1)
                TutorialManager.TargetPawns.RemoveAt(0);
        }
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

        if (Physics.Raycast(rayB, out raycastHitB)) _limiterB = raycastHitB.point;
        else //if raycast doesn't hit anything it means the main sushi is free and level was completed
        {
            _levelComplete = true;
            LevelManager.GameState = GameState.GameOver;
            OnLevelComplete?.Invoke();
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

    private void SlideTo(Vector3 targetPosition) => transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * _speed);

    private Vector3 GetPointerWorldPosition() => _camera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Vector3.Distance(transform.position, _camera.transform.position)));



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
