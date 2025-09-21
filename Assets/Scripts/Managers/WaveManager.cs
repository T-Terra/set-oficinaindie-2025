using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] int _timeBetweenWaves = 20;
    [SerializeField] float _currentTime = 0f;
    [SerializeField] int _currentWave = 0;

    [Header("Tilemap")]
    [SerializeField] Grid _grid;
    [SerializeField] List<Enemy> _enemies;

    [Header("Enemy Prefabs")]
    [SerializeField] GameObject _warriorPrefab;
    [SerializeField] GameObject _archerPrefab;
    [SerializeField] GameObject _tankPrefab;

    [Serializable]
    enum Spawn
    {
        Empty,
        Warrior,
        Archer,
        Tank,
    }

    [Serializable]
    class SpawnRow
    {
        public List<Spawn> row = new();

        public SpawnRow(params Spawn[] types)
        {
            row = new List<Spawn>(types);
        }
    }

    [Header("Waves")]
    [SerializeField]
    List<SpawnRow> spawnOrder = new()
    {
        new SpawnRow( Spawn.Empty  , Spawn.Empty  , Spawn.Warrior, Spawn.Empty  , Spawn.Empty  , Spawn.Empty   ),
        new SpawnRow( Spawn.Empty  , Spawn.Empty  , Spawn.Archer , Spawn.Empty  , Spawn.Empty  , Spawn.Empty   ),
        new SpawnRow( Spawn.Empty  , Spawn.Empty  , Spawn.Tank   , Spawn.Empty  , Spawn.Empty  , Spawn.Empty   ),
        new SpawnRow( Spawn.Empty  , Spawn.Tank   , Spawn.Warrior, Spawn.Archer , Spawn.Empty  , Spawn.Empty   ),
        new SpawnRow( Spawn.Tank   , Spawn.Warrior, Spawn.Archer , Spawn.Warrior, Spawn.Tank   , Spawn.Warrior ),
        new SpawnRow( Spawn.Empty  , Spawn.Tank   , Spawn.Warrior, Spawn.Warrior, Spawn.Tank   , Spawn.Empty   ),
    };

    async void SpawnWave()
    {
        // Small delay before spawning to wait for move animation
        await System.Threading.Tasks.Task.Delay(1000);

        if (_currentWave >= spawnOrder.Count) return;

        for (int x = 0; x < 6; x++)
        {
            Vector2Int cellPosition = new(x, 7);

            Vector2 gridSize = new(_grid.cellSize.x, _grid.cellSize.y);
            Vector2 gridCenter = new(_grid.transform.position.x, _grid.transform.position.y);

            Vector2 worldPosition = cellPosition - new Vector2Int(3, 4) + new Vector2(0.5f, 0.5f);
            worldPosition = gridCenter + worldPosition * gridSize;

            int index = x;
            if (index < 0 || index >= spawnOrder[_currentWave].row.Count) continue;

            Spawn spawnType = spawnOrder[_currentWave].row[index];
            if (spawnType == Spawn.Empty) continue;

            GameObject enemyObject = Instantiate(SelectSpawn(spawnType), worldPosition, Quaternion.identity, _grid.transform);
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            enemy.moveDistance = gridSize.y;
            enemy.OnSpawn(new Vector2Int(x, 7));

            _enemies.Add(enemy);
        }
    }

    GameObject SelectSpawn(Spawn type)
    {
        return type switch
        {
            Spawn.Warrior => _warriorPrefab,
            Spawn.Archer => _archerPrefab,
            Spawn.Tank => _tankPrefab,
            _ => null,
        };
    }


    void MoveWave()
    {
        // list enemies to remove
        List<Enemy> toRemove = new();
        foreach (var enemy in _enemies)
        {
            if (enemy == null)
            {
                toRemove.Add(enemy);
                continue;
            }

            enemy.OnMove(Vector2Int.down);
        }

        foreach (var enemy in toRemove)
        {
            _enemies.Remove(enemy);
        }

    }

    public static Action OnWaveStart;

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime < _currentWave * _timeBetweenWaves) return;
        _currentWave++;


        OnWaveStart?.Invoke();
        MoveWave();
        SpawnWave();

    }
}