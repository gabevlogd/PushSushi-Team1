using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class LevelEditorTool : EditorWindow
{
    private Grid<Toogle> _gridCoordinates = new Grid<Toogle>(6, 6, 1, new Vector3(-3f, 0f, -3f), (int x, int y) => new Toogle(x, y));
    private List<Toogle> _selectedCoordinates;
    private List<SlidableComponent> _placedSushis;
    private string _newLevelSceneName;
    private string[] _sushiMeshs = new string[]
    {
        "MainSushi", "MainSushi 1", "MainSushi 2", "MainSushi 3", "MainSushi 4", "MainSushi 5", "MainSushi 6", "MainSushi 7", "MainSushi 8", "MainSushi 9",
        "ShortSushi", "ShortSushi 1", "ShortSushi 2", "ShortSushi 3",
        "LongSushi", "LongSushi 1", "LongSushi 2", "LongSushi 3"
    };
    private int _index = 0;

    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow() => GetWindow<LevelEditorTool>("Level Editor");
    
    void OnGUI()
    {
        DrawNewLevelButton();
        DrawSaveLevelButton();
        DrawNewLevelNameLabel();
        DrawTogglesGrid();
        DrawMeshSelectorPopUp();
        DrawPlacerButton();
        DrawBackButton();
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
    private void DrawNewLevelNameLabel()
    {
        GUILayout.Label("New level scene name: ");
        GUILayout.BeginHorizontal();
        _newLevelSceneName = GUILayout.TextField(_newLevelSceneName);
        GUILayout.EndHorizontal();
    }
    private void DrawMeshSelectorPopUp()
    {
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Sushi's mesh:");
        _index = EditorGUILayout.Popup(_index, _sushiMeshs);
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

        Quaternion targetRotation = GetTargetRotation(sushiToPlace);
        Vector3 targetPosition = GetTargetPosition(sushiToPlace, xCoo, yCoo);

        SlidableComponent placedSushi = Instantiate(sushiToPlace, targetPosition, targetRotation);

        if (_placedSushis == null)
            _placedSushis = new List<SlidableComponent>();

        _placedSushis.Add(placedSushi);
    }
    private Quaternion GetTargetRotation(SlidableComponent sushiToPlace)
    {
        Quaternion targetRotation;
        if (_selectedCoordinates[1].x == _selectedCoordinates[0].x)
        {
            sushiToPlace.SlidingDirection = SlidingDirection.Vertical;
            targetRotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else
        {
            sushiToPlace.SlidingDirection = SlidingDirection.Horizontal;
            targetRotation = Quaternion.identity;
        }
        return targetRotation;
    }
    private Vector3 GetTargetPosition(SlidableComponent sushiToPlace, int xCoo, int yCoo)
    {
        BoxCollider collider = sushiToPlace.GetComponent<BoxCollider>();
        Vector3 targetPosition;
        if (collider.size.x == 2)
            targetPosition = _gridCoordinates.GetWorldPosition(xCoo, yCoo);
        else
        {
            if (sushiToPlace.SlidingDirection == SlidingDirection.Vertical)
                targetPosition = _gridCoordinates.GetWorldPosition(xCoo, yCoo + 1);
            else
                targetPosition = _gridCoordinates.GetWorldPosition(xCoo + 1, yCoo);
        }
        return targetPosition;
    }
    private void PerformPlacerButton()
    {
        if (ValidCordinates())
            PlaceSushi(Resources.Load<SlidableComponent>(_sushiMeshs[_index]));

        for(int x = 0; x < _gridCoordinates.GetWidth(); x++)
        {
            for (int y = 0; y < _gridCoordinates.GetHeight(); y++)
            {
                _gridCoordinates.GetGridObject(x, y).Value = false;
            }
        }
        
    }
    private void PerformNewLevelButton()
    {
        _newLevelSceneName = "";
        EditorSceneManager.OpenScene("Assets/Scenes/EmptyLevelTemplate.unity");
    }
    private void PerformSaveLevelButton()
    {
        if (_newLevelSceneName == "" || _newLevelSceneName == null)
        {
            Debug.Log("Name the scene before saving");
            return;
        }
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), $"Assets/Scenes/Levels/{_newLevelSceneName}.unity", true);
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
                DestroyImmediate(sushi.gameObject);
            }
            
        }
    }
}
