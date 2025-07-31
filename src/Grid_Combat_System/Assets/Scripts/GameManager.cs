using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private List<Cell> _cells = new List<Cell>();
    [SerializeField] private List<Player> _playerPrefabs = new List<Player>();
    [SerializeField] private List<Enemy> _enemyPrefabs = new List<Enemy>();
    [SerializeField] private GamePhase _gamePhase;
    // ReSharper disable once InconsistentNaming
    public static GameManager Instance;

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
        if (_gamePhase == GamePhase.SpawnPhase) {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        if (Input.GetMouseButtonUp(0)) {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            var cell = hit.collider.GetComponent<Cell>();
            if (cell.GetCellType() == CellType.Spawner) {
                var playerPrefab = GetPlayerPrefab();
                Debug.Log(playerPrefab.transform.localScale);
                Vector3 playerOffset = new Vector3(0, 0, -playerPrefab.transform.localScale.z/2);
                var entitySpawned = Instantiate(GetPlayerPrefab(), cell.transform.position, Quaternion.identity , cell.transform);
                entitySpawned.transform.localPosition += playerOffset;
                cell.SetEntityOnCell(entitySpawned);
            }
        }
    }

    public GamePhase GetGamePhase()
    {
        return _gamePhase;
    }

    public Player GetPlayerPrefab()
    {
        _gamePhase = GamePhase.PlayerTurn;
        return _playerPrefabs[0];
    }
}

[Serializable]
public enum GamePhase
{
    SpawnPhase,
    PlayerTurn,
    EnemyTurn
}


