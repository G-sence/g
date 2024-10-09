using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 敵の最大HP
    public int maxHP = 10;
    // 現在のHP
    private int currentHP;
    // プレイヤーのTransform
    private Transform playerTransform;
    // 敵の移動速度
    public float moveSpeed = 3f;
    // 弾道移動の高さ
    public float arcHeight = 5f;

    private Vector3 startPoint;
    private Vector3 targetPosition;
    private float timeElapsed;
    private float travelTime;

    void Start()
    {
        currentHP = maxHP; // HPを初期化
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // プレイヤーの位置を取得
        startPoint = transform.position;
        if (playerTransform != null)
        {
            targetPosition = playerTransform.position + new Vector3(2, -1, 0); // 初期位置でプレイヤーの位置を一度だけ取得
        }
        travelTime = Vector3.Distance(startPoint, targetPosition) / moveSpeed;
        timeElapsed = 0f;
    }

    void Update()
    {
        if (targetPosition != null)
        {
            // 経過時間を更新
            timeElapsed += Time.deltaTime;
            float progress = timeElapsed / travelTime;
            if (progress > 1f) progress = 1f;

            // プレイヤーに向かう弾道移動
            Vector3 nextPosition = Vector3.Lerp(startPoint, targetPosition, progress);
            nextPosition.y += Mathf.Sin(progress * Mathf.PI) * arcHeight;

            transform.position = nextPosition;

            // プレイヤーの方向に向けて回転
            Quaternion lookRotation = Quaternion.LookRotation(targetPosition - transform.position);
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
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(100); // 敵を倒したら100点加算
        }
        Destroy(gameObject); // 敵オブジェクトを破壊
    }

    // 火焰やレーザーと触れたときにダメージを受ける
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Laser") || collision.gameObject.CompareTag("Fire"))
        {
            TakeDamage(10); // 火焰またはレーザーに当たったら1ダメージ
        }
    }
}
