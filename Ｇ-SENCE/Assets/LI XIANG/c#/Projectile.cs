using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 弾丸の速度（Inspectorで編集可能）
    public float speed = 1f;
    // プレイヤーに当たった場合のダメージ
    public int damage = 1;
    // 追尾対象
    private Transform target;

    void Update()
    {
        // ターゲットが設定されている場合、ターゲットの方向に移動
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    // ターゲットを設定するメソッド
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たった場合
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーにダメージを与える
            PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
            if (player != null)
            {
                player.TakeDamage(damage); // プレイヤーのTakeDamageメソッドを呼び出し
            }
            // 弾丸を破壊
            Destroy(gameObject);
        }
        // プレイヤー以外に当たった場合
        else if (!collision.gameObject.CompareTag("Enemy")) // 敵以外に衝突した場合のみ破壊
        {
            // 弾丸を破壊
            Destroy(gameObject);
        }
    }
}