using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private List<Cell> _cells = new List<Cell>();
    [SerializeField] private List<GameObject> _playerPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> _enemyPrefabs = new List<GameObject>();
    [SerializeField] private GamePhase _gamePhase;
    // ReSharper disable once InconsistentNaming
    public static GridManager Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(Instance);
        }

        foreach (GameObject obj in _gridGenerator.GetCells())
        {
            if (obj != null) {
                _cells.Add(obj.GetComponent<Cell>());
            }
        }

        _gamePhase = GamePhase.SpawnPhase;
    }


    private void Update()
    {

    }

    public GamePhase GetGamePhase()
    {
        return _gamePhase;
    }

    public GameObject GetPlayerPrefab()
    {
        _gamePhase = GamePhase.PlayPhase;
        return _playerPrefabs[0];
    }
}

[Serializable]
public enum GamePhase
{
    SpawnPhase,
    PlayPhase
}


