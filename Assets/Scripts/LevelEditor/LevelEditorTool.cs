#if UNITY_EDITOR
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class LevelEditorTool : EditorWindow
{
    private Grid<Toogle> _gridCoordinates = new Grid<Toogle>(6, 6, 1, new Vector3(-3f, 0f, -3f), (int x, int y) => new Toogle(x, y));
    private List<Toogle> _selectedCoordinates;
    private List<SlidableComponent> _placedSushis;
    private List<string> _placedSushisMeshs;

    private string[] _sushiMeshs = new string[]
    {
        "MainSushi", "MainSushi 1", "MainSushi 2", "MainSushi 3", "MainSushi 4", "MainSushi 5", "MainSushi 6", "MainSushi 7", "MainSushi 8", "MainSushi 9",
        "ShortSushi", "ShortSushi 1", "ShortSushi 2", "ShortSushi 3",
        "LongSushi", "LongSushi 1", "LongSushi 2", "LongSushi 3"
    };
    private int _meshIndex = 0;

    private Difficulty _levelDifficulty;
    private Vector2 _scrollPos;

    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow() => GetWindow<LevelEditorTool>("Level Editor");
    
    void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        DrawNewLevelButton();
        DrawSaveLevelButton();
        DrawTogglesGrid();
        DrawLevelDifficultyEnum();
        DrawMeshSelectorPopUp();
        DrawPlacerButton();
        DrawBackButton();
        EditorGUILayout.EndScrollView();
    }

    private void DrawNewLevelButton()
    {
        GUILayout.Space(20f);
        if (GUILayout.Button("New level"))
            PerformNewLevelButton();
    }
    private void DrawSaveLevelButton()
    {
        GUILayout.Space(10f);
        if (GUILayout.Button("Save level"))
            PerformSaveLevelButton();
        GUILayout.Space(20f);
    }
    private void DrawLevelDifficultyEnum()
    {
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Level difficulty: ");
        _levelDifficulty = (Difficulty)EditorGUILayout.EnumPopup(_levelDifficulty);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    private void DrawMeshSelectorPopUp()
    {
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Sushi's mesh:");
        _meshIndex = EditorGUILayout.Popup(_meshIndex, _sushiMeshs);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    private void DrawPlacerButton()
    {
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Place sushi"))
            PerformPlacerButton();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void DrawBackButton()
    {
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Back"))
            PerformBackButton();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }


    private void PerformPlacerButton()
    {
        if (ValidCordinates())
            PlaceSushi(Resources.Load<SlidableComponent>(_sushiMeshs[_meshIndex]));

        for (int x = 0; x < _gridCoordinates.GetWidth(); x++)
        {
            for (int y = 0; y < _gridCoordinates.GetHeight(); y++)
            {
                _gridCoordinates.GetGridObject(x, y).Value = false;
            }
        }

    }
    private void PerformNewLevelButton()
    {
        _placedSushis = null;
        _placedSushisMeshs = null;
        EditorSceneManager.OpenScene("Assets/Scenes/EmptyLevelTemplate.unity");
    }
    private void PerformSaveLevelButton()
    {
        LevelData newLevel = CreateNewLevel();
        if (newLevel == null) return;
        AssetDatabase.CreateAsset(newLevel, $"Assets/Resources/{newLevel.Difficulty}/Level {newLevel.LevelIndex}.asset");
        AssetDatabase.SaveAssets();
        Debug.Log("New level saved");
    }
    private void PerformBackButton()
    {
        if (_placedSushis == null) return;

        if (_placedSushis.Count > 0)
        {
            SlidableComponent sushi = _placedSushis[_placedSushis.Count - 1];
            if (sushi != null)
            {
                _placedSushis.Remove(sushi);
                _placedSushisMeshs.RemoveAt(_placedSushisMeshs.Count - 1);
                DestroyImmediate(sushi.gameObject);
            }

        }
    }



    /// <summary>
    /// Draws a grid of toggle for the visual selection of the coordinates
    /// </summary>
    private void DrawTogglesGrid()
    {
        GUILayout.Space(20f);
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
    /// <summary>
    /// Returns true if the selected coordinates are valid
    /// </summary>
    /// <returns></returns>
    private bool ValidCordinates()
    {
        _selectedCoordinates = GetSelectedCoordinates();
        if (_selectedCoordinates.Count < 2 || _selectedCoordinates.Count > 3)
        {
            Debug.Log("Invalid coordinates");
            return false;
        }

        List<Toogle> coordinatesSortByX = _selectedCoordinates.OrderBy(coo => coo.x).ToList();
        List<Toogle> coordinatesSortByY = _selectedCoordinates.OrderBy(coo => coo.y).ToList();

        int xCoo = coordinatesSortByX[0].x;
        int yCoo = coordinatesSortByY[0].y;

        foreach (Toogle i in _selectedCoordinates)
        {
            if (i.x == xCoo) continue;
            else
            {
                foreach(Toogle j in _selectedCoordinates)
                {
                    if (j.y == yCoo) continue;
                    else
                    {
                        Debug.Log("Invalid coordinates");
                        return false;
                    }
                }
                if (coordinatesSortByX[coordinatesSortByX.Count - 1].x - xCoo == 2 || coordinatesSortByX[coordinatesSortByX.Count - 1].x - xCoo == 1)
                {
                    _selectedCoordinates = coordinatesSortByX;
                    return true;
                }
                else
                {
                    Debug.Log("Invalid coordinates");
                    return false;
                }
            }
        }

        if (coordinatesSortByY[coordinatesSortByY.Count - 1].y - yCoo == 2 || coordinatesSortByY[coordinatesSortByY.Count - 1].y - yCoo == 1)
        {
            _selectedCoordinates = coordinatesSortByY;
            return true;
        }
        else
        {
            Debug.Log("Invalid coordinates");
            return false;
        }
    }
    /// <summary>
    /// Returns a list of all selected coordinates
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Instantiates the passed sushi on the map
    /// </summary>
    private void PlaceSushi(SlidableComponent sushiToPlace)
    {
        int xCoo = _selectedCoordinates[0].x;
        int yCoo = _selectedCoordinates[0].y;

        Quaternion targetRotation = GetTargetRotation();
        Vector3 targetPosition = GetTargetPosition(sushiToPlace, xCoo, yCoo, targetRotation.eulerAngles.y != 0f);

        SlidableComponent placedSushi = Instantiate(sushiToPlace, targetPosition, targetRotation);

        if (_placedSushis == null)
            _placedSushis = new List<SlidableComponent>();

        if (_placedSushisMeshs == null)
            _placedSushisMeshs = new List<string>();

        _placedSushis.Add(placedSushi);
        _placedSushisMeshs.Add(_sushiMeshs[_meshIndex]);
    }
    private Quaternion GetTargetRotation()
    {
        Quaternion targetRotation;
        if (_selectedCoordinates[1].x == _selectedCoordinates[0].x)
            targetRotation = Quaternion.Euler(0f, -90f, 0f);
        else
            targetRotation = Quaternion.identity;
        
        return targetRotation;
    }
    private Vector3 GetTargetPosition(SlidableComponent sushiToPlace, int xCoo, int yCoo, bool vertical)
    {
        BoxCollider collider = sushiToPlace.GetComponent<BoxCollider>();
        Vector3 targetPosition;
        if (collider.size.x == 2)
            targetPosition = _gridCoordinates.GetWorldPosition(xCoo, yCoo);
        else
        {
            if (vertical)
                targetPosition = _gridCoordinates.GetWorldPosition(xCoo, yCoo + 1);
            else
                targetPosition = _gridCoordinates.GetWorldPosition(xCoo + 1, yCoo);
        }
        return targetPosition;
    }
    private LevelData CreateNewLevel()
    {
        LevelData newLevel = CreateInstance<LevelData>();
        SlidableComponent[] pawns = FindObjectsByType<SlidableComponent>(FindObjectsSortMode.InstanceID);
        if (pawns.Length == 0 || _placedSushisMeshs == null || _placedSushisMeshs.Count == 0) return null;
        newLevel.PawnsPositions = new Vector3[pawns.Length];
        newLevel.PawnsRotations = new Quaternion[pawns.Length];
        newLevel.Pawn = new SlidableComponent[pawns.Length];
        newLevel.Difficulty = _levelDifficulty;
        for (int i = 0; i < pawns.Length; i++)
        {
            if (_placedSushisMeshs[i][0].ToString() == "M")
                newLevel.MainPawn = Resources.Load<SlidableComponent>(_placedSushisMeshs[i]);

            newLevel.Pawn[i] = Resources.Load<SlidableComponent>(_placedSushisMeshs[pawns.Length - i - 1]);
            newLevel.PawnsPositions[i] = pawns[i].transform.position;
            newLevel.PawnsRotations[i] = pawns[i].transform.rotation;
        }
        DirectoryInfo info = new DirectoryInfo($"Assets/Resources/{newLevel.Difficulty}");
        newLevel.LevelIndex = (int)(info.GetFiles().Length * 0.5f + 1); // * 0.5 because of meta files
        return newLevel;
    }
}

#endif
