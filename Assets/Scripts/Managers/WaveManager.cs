using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManager : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] int _timeBetweenWaves = 20;
    [SerializeField] float _currentTime = 0f;
    [SerializeField] int _currentWave = 0;

    [Header("Tilemap")]
    [SerializeField] Tilemap _tilemap;
    [SerializeField] List<Enemy> _enemies;

    [Header("Enemy Prefabs")]
    [SerializeField] GameObject _warriorPrefab;
    [SerializeField] GameObject _archerPrefab;
    [SerializeField] GameObject _tankPrefab;

    [System.Serializable]
    enum Spawn
    {
        Empty,
        Warrior,
        Archer,
        Tank,
    }

    [System.Serializable]
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

    void SpawnWave()
    {
        if (_currentWave >= spawnOrder.Count) return;

        for (int x = 0; x < 6; x++)
        {
            Vector3Int cellPosition = new(x, 7);
            // if (!_tilemap.HasTile(cellPosition)) continue;

            Vector3 worldPosition = _tilemap.CellToWorld(cellPosition);
            worldPosition += new Vector3(0.5f, 0.5f, 0f); // center of tile

            int index = x;
            if (index < 0 || index >= spawnOrder[_currentWave].row.Count) continue;

            Spawn spawnType = spawnOrder[_currentWave].row[index];
            if (spawnType == Spawn.Empty) continue;

            GameObject enemyObject = Instantiate(SelectSpawn(spawnType), worldPosition, Quaternion.identity, transform);
            Enemy enemy = enemyObject.GetComponent<Enemy>();

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
        int enemiesCount = _enemies.Count;
        for (int i = 0; i < enemiesCount; i++)
        {
            Enemy enemy = _enemies[i];

            if (enemy == null)
            {
                _enemies.RemoveAt(i);
                continue;
            }

            enemy.OnMove(Vector2Int.down);
        }
    }



    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime < _currentWave * _timeBetweenWaves) return;
        _currentWave++;

        // to update wave:
        MoveWave();
        SpawnWave();

    }
}