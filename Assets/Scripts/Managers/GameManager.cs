using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public UndoManager Undo;
    public List<SlidableComponent> SlidableObjects;
    public int MoveCount;
    
    private void Awake()
    {
        Undo = new UndoManager();
        SlidableObjects = new List<SlidableComponent>(FindObjectsOfType<SlidableComponent>());
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Undo.Start();
        
        List<ObjectState> objectStates = new List<ObjectState>();

        foreach (SlidableComponent component in SlidableObjects)
        {
            ObjectState objState = new ObjectState
            {
                Position = component.transform.position,
                Rotation = component.transform.rotation,
            };

            objectStates.Add(objState);
        }
        
        MoveCount = Undo.GetMoveCount();
        
        GameState currentState = new GameState(objectStates);
        Undo.RecordGameState(currentState);
    }
}