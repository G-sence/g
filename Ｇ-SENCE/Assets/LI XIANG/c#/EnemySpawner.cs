using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 敵のプレハブ
    public GameObject enemyPrefab;
    // 敵を生成する間隔（秒）
    public float spawnInterval = 5f;
    // 敵の生成位置
    public Transform spawnPoint;

    void Start()
    {
        // 一定間隔で敵を生成する
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    // 敵を生成するメソッド
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
