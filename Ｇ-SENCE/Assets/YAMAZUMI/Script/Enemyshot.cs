using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    // 弾丸のPrefab
    public GameObject projectilePrefab;
    // プレイヤーのTransform
    private Transform playerTransform;
    // 発射間隔の制御用
    private float elapsedTime;
    // 発射頻度（Inspectorで編集可能）
    public float fireInterval = 3f;

    void Start()
    {
        // プレイヤーの位置を取得
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // 発射タイマー初期化
        elapsedTime = -5.0f; // ゲーム開始後5秒後に発射開始
    }

    void Update()
    {
        // 経過時間を更新
        elapsedTime += Time.deltaTime;

        // 発射間隔をfireIntervalで調整しながら攻撃
        if (elapsedTime > fireInterval)
        {
            FireProjectile();
            elapsedTime = 0; // 発射後、時間をリセット
        }
    }

    // プレイヤーに向けて追尾弾を発射
    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(playerTransform); // プレイヤーをターゲットに設定
        }
        Destroy(projectile, 3f); // 弾丸を3秒後に破壊
    }
}