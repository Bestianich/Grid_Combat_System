using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

        _gamePhase = GamePhase.SpawnPhase;
    }





    public void SetGamePhase(GamePhase gamePhase)
    {
        _gamePhase = gamePhase;
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


