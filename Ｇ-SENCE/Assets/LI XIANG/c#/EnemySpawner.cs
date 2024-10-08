using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // ���Υץ�ϥ�
    public GameObject enemyPrefab;
    // �������ɤ����g�����룩
    public float spawnInterval = 5f;
    // ��������λ��
    public Transform spawnPoint;

    void Start()
    {
        // һ���g���ǔ������ɤ���
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    // �������ɤ���᥽�å�
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
