using Core.Enemies;
using Staff.Pool;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Pool enemiesPool;
    [SerializeField] private int spawnStartCount = 1;
    [SerializeField] private float spawnInterval = 2;

    private FieldGenerator fieldGenerator;
    private bool wasDestroyed;

    private void Start()
    {
        fieldGenerator = FindObjectOfType<FieldGenerator>();
        for (int i = 0; i < spawnStartCount; ++i)
            SpawnEnemy();
        if (0.0f < spawnInterval)
            InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    private void OnDestroy()
    {
        wasDestroyed = true;
    }

    private void SpawnEnemy()
    {
        var enemy = enemiesPool.Pop<Enemy>();
        enemy.Init(fieldGenerator.FieldCenter, fieldGenerator.FieldSize);
        enemy.Destroyed += OnEnemyWasDestroyed;
    }

    private void OnEnemyWasDestroyed(Enemy sender)
    {
        sender.Destroyed -= OnEnemyWasDestroyed;
        if (!wasDestroyed)
            SpawnEnemy();
    }
}