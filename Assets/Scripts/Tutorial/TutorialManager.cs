using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    private LevelData _currentLevel;
    private List<int> _pawnsToSlideIndex;
    private SlidableComponent _pawnToSlide;
    private bool _slidePawn;
    private bool _pawnGrabbed;
    [SerializeField]
    private float _speed;
    private Vector3 _targetPosition;
    private event Action _onTargetPawnSelected;

    public static List<SlidableComponent> a;

    private void Awake()
    {
        if (LevelManager.GameState == GameState.Tutorial)
        {
            //_currentLevel = LevelLoader.LevelToLoad;
            //InitTutorial();
            a = new List<SlidableComponent>();
            SlidableComponent[] pawns = FindObjectsOfType<SlidableComponent>();
            foreach (SlidableComponent pawn in pawns)
                if (pawn.transform.rotation.eulerAngles.y != 0)
                    a.Add(pawn);
        }
    }

    private void Update()
    {
        //Boh();
        //_onTargetPawnSelected?.Invoke();
    }

    private void InitTutorial()
    {
        _pawnsToSlideIndex = new List<int>();

        for (int i = 0; i < _currentLevel.Pawn.Length; i++)
        {
            if (_currentLevel.PawnsRotations[i].eulerAngles.y != 0)
                _pawnsToSlideIndex.Add(i);
        }
    }

    private void Boh()
    {
        if (Input.touchCount == 0) return;
        //if (_slidePawn) return;
        if (_pawnGrabbed) return;

        Vector2 touchPosition = Input.GetTouch(0).position;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.transform != null && hitInfo.transform.position == _currentLevel.PawnsPositions[_pawnsToSlideIndex[0]])
        {
            Debug.Log("hit");
            _pawnToSlide = hitInfo.collider.GetComponent<SlidableComponent>();
            _pawnToSlide.OnTutorial = true;
            //_targetPosition = new Vector3(_pawnToSlide.transform.position.x, _pawnToSlide.transform.position.y, -1.5f);
            //_onTargetPawnSelected = SlideTargetPawn;
            _pawnGrabbed = true;
            //_slidePawn = true;
        }
    }

    //private void SlideTargetPawn()
    //{
    //    //CheckForSlideRequest();

    //    if (_slidePawn)
    //    {
    //        _pawnToSlide.transform.position = Vector3.MoveTowards(_pawnToSlide.transform.position, _targetPosition, Time.deltaTime * _speed);
    //        if (Vector3.Distance(_pawnToSlide.transform.position, _targetPosition) < 0.1f)
    //        {
    //            _pawnToSlide.transform.position = _targetPosition;
    //            _slidePawn = false;
    //            _pawnGrabbed = false;
    //            _pawnsToSlideIndex.RemoveAt(0);
    //        }
    //    }
    //}

    //private void CheckForSlideRequest()
    //{
    //    if (Input.GetTouch(0).phase == TouchPhase.Ended)
    //        _slidePawn = true;
    //}
}
