using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditorTool : EditorWindow
{
    private bool[,] gridCoordinates = new bool[6, 6];
    private Grid<Toogle> _gridCoordinates = new Grid<Toogle>(6, 6, 1, Vector3.zero, (int x, int y) => new Toogle(x, y));


    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow() => GetWindow<LevelEditorTool>("Level Editor");
    


    void OnGUI()
    {
        DrawTogglesGrid();

        var e = Event.current;

        //if (e.type == EventType.MouseUp /*&& e.button == 0*/)
            //Debug.Log("Right mouse button lifted");

        if (GUILayout.Button("debug"))
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 6; y++)
                    Debug.Log($"{x}, {y},  {_gridCoordinates.GetGridObject(x, y).Value}");
            }
        }
    }

    
    private void DrawTogglesGrid()
    {
        
        GUILayout.BeginVertical();
        for (int y = 5; y > -1; y--)
        {
            GUILayout.BeginHorizontal();

            for (int x = 0; x < 6; x++)
                _gridCoordinates.GetGridObject(x, y).Value = GUILayout.Toggle(_gridCoordinates.GetGridObject(x, y).Value, "", GUILayout.Width(20f), GUILayout.Height(20f));
            
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private bool ValidCordinates()
    {
        //List<bool> selectedCoordinates = GetSelectedCoordinates();
        //if (selectedCoordinates.Count < 2 || selectedCoordinates.Count > 3) return false;

        return true;
    }

    //private List<bool> GetSelectedCoordinates()
    //{
    //    List<Toogle> selectedCoordinates = new List<Toogle>();
    //    for(int x = 0; x < _gridCoordinates.GetWidth(); x++)
    //    {
    //        for (int y = 0; y < _gridCoordinates.GetHeight(); y++)
    //        {
    //            if (_gridCoordinates.GetGridObject(x,y).Value)
                    
    //        }
    //    }

    //}
}
