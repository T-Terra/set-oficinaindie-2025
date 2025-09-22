using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] int _timeBetweenWaves = 20;
    [SerializeField] float _currentTime = 0f;
    [SerializeField] int _currentWave = 0;
    [SerializeField] TMP_Text Text_Wave;
    [SerializeField] TMP_Text Text_Time;

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
        new SpawnRow( Spawn.Archer , Spawn.Empty  , Spawn.Warrior, Spawn.Empty  , Spawn.Empty  , Spawn.Empty   ),
        new SpawnRow( Spawn.Empty  , Spawn.Empty  , Spawn.Archer , Spawn.Empty  , Spawn.Empty  , Spawn.Empty   ),
        new SpawnRow( Spawn.Empty  , Spawn.Empty  , Spawn.Tank   , Spawn.Empty  , Spawn.Empty  , Spawn.Empty   ),
        new SpawnRow( Spawn.Empty  , Spawn.Tank   , Spawn.Warrior, Spawn.Archer , Spawn.Empty  , Spawn.Empty   ),
        new SpawnRow( Spawn.Tank   , Spawn.Warrior, Spawn.Archer , Spawn.Warrior, Spawn.Tank   , Spawn.Warrior ),
        new SpawnRow( Spawn.Empty  , Spawn.Tank   , Spawn.Warrior, Spawn.Warrior, Spawn.Tank   , Spawn.Empty   ),
    };

    private System.Collections.IEnumerator SpawnWave()
    {
        // Small delay before spawning to wait for move animation
        yield return new WaitForSeconds(1f);
        //await System.Threading.Tasks.Task.Delay(1000);
        // If current wave is beyond defined spawnOrder, generate a random row
        bool useDefinedRow = _currentWave - 1 >= 0 && _currentWave - 1 < spawnOrder.Count;

        for (int x = 0; x < 6; x++)
        {
            Vector2Int cellPosition = new(x, 7);

            Vector2 gridSize = new(_grid.cellSize.x, _grid.cellSize.y);
            Vector2 gridCenter = new(_grid.transform.position.x, _grid.transform.position.y);

            Vector2 worldPosition = cellPosition - new Vector2Int(3, 4) + new Vector2(0.5f, 0.5f);
            worldPosition = gridCenter + worldPosition * gridSize;

            Spawn spawnType;

            if (useDefinedRow)
            {
                int index = x;
                if (index < 0 || index >= spawnOrder[_currentWave - 1].row.Count) continue;

                spawnType = spawnOrder[_currentWave - 1].row[index];
            }
            else
            {
                // Not in spawnOrder: generate a random spawn with Empty having 3x weight
                spawnType = PickWeightedSpawn();
            }

            if (spawnType == Spawn.Empty) continue;

            GameObject enemyObject = Instantiate(SelectSpawn(spawnType), worldPosition, Quaternion.identity, _grid.transform);
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            enemy.moveDistance = gridSize.y;
            enemy.OnSpawn(new Vector2Int(x, 7));

            _enemies.Add(enemy);
        }
    }

    // Picks a spawn type randomly where Empty has triple the chance of other types
    Spawn PickWeightedSpawn()
    {
        // Define weights: Empty = 3, Warrior = 1, Archer = 1, Tank = 1
        int emptyWeight = 3;
        int warriorWeight = 1;
        int archerWeight = 1;
        int tankWeight = 1;

        int total = emptyWeight + warriorWeight + archerWeight + tankWeight;
        int r = UnityEngine.Random.Range(0, total);

        if (r < emptyWeight) return Spawn.Empty;
        r -= emptyWeight;
        if (r < warriorWeight) return Spawn.Warrior;
        r -= warriorWeight;
        if (r < archerWeight) return Spawn.Archer;
        r -= archerWeight;
        return Spawn.Tank;
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
        Text_Wave.text = _currentWave.ToString();
        Text_Time.text = _currentTime.ToString("0");
        _currentTime += Time.deltaTime;
        if (_currentTime < _currentWave * _timeBetweenWaves) return;
        _currentWave++;


        OnWaveStart?.Invoke();
        MoveWave();
        StartCoroutine(SpawnWave());
    }
}