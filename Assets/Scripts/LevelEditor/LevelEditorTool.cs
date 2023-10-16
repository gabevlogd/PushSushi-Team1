using System.Linq;
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


        if (GUILayout.Button("debug"))
        {
            //for (int x = 0; x < 6; x++)
            //{
            //    for (int y = 0; y < 6; y++)
            //        Debug.Log($"{x}, {y},  {_gridCoordinates.GetGridObject(x, y).Value}");
            //}
            //Debug.Log(ValidCordinates());
        }
    }

    
    private void DrawTogglesGrid()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Coordinates on the game grid:");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        for (int y = 5; y > -1; y--)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            for (int x = 0; x < 6; x++)
                _gridCoordinates.GetGridObject(x, y).Value = GUILayout.Toggle(_gridCoordinates.GetGridObject(x, y).Value, "", GUILayout.Width(20f), GUILayout.Height(20f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private bool ValidCordinates()
    {
        List<Toogle> selectedCoordinates = GetSelectedCoordinates();
        if (selectedCoordinates.Count < 2 || selectedCoordinates.Count > 3) return false;

        List<Toogle> coordinatesSortByX = selectedCoordinates.OrderBy(coo => coo.x).ToList();
        List<Toogle> coordinatesSortByY = selectedCoordinates.OrderBy(coo => coo.y).ToList();

        int xCoo = coordinatesSortByX[0].x;
        int yCoo = coordinatesSortByY[0].y;

        foreach (Toogle i in selectedCoordinates)
        {
            if (i.x == xCoo) continue;
            else
            {
                foreach(Toogle j in selectedCoordinates)
                {
                    if (j.y == yCoo) continue;
                    else return false;
                }
                if (coordinatesSortByX[coordinatesSortByX.Count - 1].x - xCoo == 2 || coordinatesSortByX[coordinatesSortByX.Count - 1].x - xCoo == 1)
                    return true;
                else return false;
            }
        }

        if (coordinatesSortByY[coordinatesSortByY.Count - 1].y - yCoo == 2 || coordinatesSortByY[coordinatesSortByY.Count - 1].y - yCoo == 1)
            return true;
        else return false;
    }

    private List<Toogle> GetSelectedCoordinates()
    {
        List<Toogle> selectedCoordinates = new List<Toogle>();
        for (int x = 0; x < _gridCoordinates.GetWidth(); x++)
        {
            for (int y = 0; y < _gridCoordinates.GetHeight(); y++)
            {
                if (_gridCoordinates.GetGridObject(x, y).Value)
                    selectedCoordinates.Add(_gridCoordinates.GetGridObject(x, y));
            }
        }
        return selectedCoordinates;

    }
}
