using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    // 敵の最大HP
    public int maxHP = 10;
    // 現在のHP
    private int currentHP;
    // スコアの参照
    public ScoreManager scoreManager;
    // プレイヤーのTransform
    private Transform playerTransform;
    // 敵の移動速度
    public float moveSpeed = 3f;

    void Start()
    {
        currentHP = maxHP; // HPを初期化
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // プレイヤーの位置を取得
    }

    void Update()
    {
        // プレイヤーに向かって移動
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // プレイヤーの方向に向けて回転
            Quaternion lookRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    // ダメージを受けるメソッド
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die(); // HPが0以下になった場合は死亡
        }
    }

    // 死亡時の処理
    void Die()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(100); // 敵を倒したら100点加算
        }
        Destroy(gameObject); // 敵オブジェクトを破壊
    }

    void OnTriggerEnter(Collider other)
    {
        // レーザーまたは火炎に当たった場合
        if (other.CompareTag("Laser") || other.CompareTag("Fire"))
        {
            TakeDamage(10); // レーザーまたは火炎に当たると1ダメージを受ける
        }
    }
}