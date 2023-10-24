using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private Coin _coinPrefab;
    [SerializeField]
    [Range(0f, 1f)]
    private float _spawnFactor;
    [SerializeField]
    [Min(1)]
    private int _maxCoinNumber;

    private List<Vector2Int> _occupiedCoordinates;
    private Grid<Tile> _grid;

    private void Awake()
    {
        _grid = new Grid<Tile>(6, 6, 1f, new Vector3(-3f, 0f, -3f), (int x, int y) => new Tile(x, y));
        _occupiedCoordinates = new List<Vector2Int>();

        TryToSpawnCoins();
    }

    private void TryToSpawnCoins()
    {
        if (Random.value < _spawnFactor)
            SpawnCoins();
    }

    private void SpawnCoins()
    {
        int numberOfCoins = Random.Range(1, _maxCoinNumber + 1);

        for(int i = 0; i < numberOfCoins; i++)
        {
            Vector2Int randomCoordinates = GetRandomCoordinates();
            Instantiate(_coinPrefab, _grid.GetWorldPosition(randomCoordinates.x, randomCoordinates.y), Quaternion.identity);
        }
    }

    private Vector2Int GetRandomCoordinates()
    {
        int x = Random.Range(0, _grid.GetWidth());
        int y = Random.Range(0, _grid.GetHeight());
        Vector2Int randomCoordinates = new Vector2Int(x, y);

        if (_occupiedCoordinates.Contains(randomCoordinates))
            randomCoordinates = GetRandomCoordinates();
        else
            _occupiedCoordinates.Add(randomCoordinates);

        return randomCoordinates;
    }
}
