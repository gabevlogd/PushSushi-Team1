using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static List<SlidableComponent> TargetPawns;
    public static SlidableComponent MainPawn;
    [SerializeField]
    private GameObject Arrow;

    private event Action OnUpdateArrow;

    private void Awake() => InitTutorial();

    private void Update() => OnUpdateArrow?.Invoke();

    private void InitTutorial()
    {
        if (LevelManager.GameState == GameState.Tutorial)
        {
            TargetPawns = new List<SlidableComponent>();
            SlidableComponent[] pawns = FindObjectsOfType<SlidableComponent>();
            foreach (SlidableComponent pawn in pawns)
            {
                if (pawn.name[0].ToString() == "L")
                    TargetPawns.Add(pawn);
                else if (pawn.name[0].ToString() == "M")
                    MainPawn = pawn;
            }

            Arrow.SetActive(true);
            OnUpdateArrow += UpdateArrowPosition;
        }
        else gameObject.SetActive(false);

        
    }

    private void UpdateArrowPosition()
    {
        if (LevelManager.GameState == GameState.GameOver)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector2 targetPawn;

        if (TargetPawns.Count == 0)
        {
            targetPawn = new Vector2(MainPawn.transform.position.x, MainPawn.transform.position.z);
            if (Arrow.transform.eulerAngles.y == 0)
            {
                Arrow.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                Arrow.transform.position += new Vector3(0f, -0.3f, 0f);
            }
        }
        else 
            targetPawn = new Vector2(TargetPawns[0].transform.position.x, TargetPawns[0].transform.position.z);

        Vector2 arrow = new Vector2(Arrow.transform.position.x, Arrow.transform.position.z);

        if (arrow != targetPawn)
            Arrow.transform.position = new Vector3(targetPawn.x, Arrow.transform.position.y, targetPawn.y);
        
    }

    
}
