using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 50;  // 敵の最大HP
    public int currentHP;  // 現在のHP
    public float moveSpeed = 3f;  // 敵の移動速度
    public float attackRange = 10f;  // 攻撃範囲
    public int attackPower = 10;  // 攻撃力
    public float attackCooldown = 2f;  // 攻撃のクールダウン時間

    public GameObject projectilePrefab;  // 発射する弾丸のプレハブ
    public Transform firePoint;  // 弾丸の発射位置
    public float projectileSpeed = 5f;  // 弾丸の速度

    private GameObject player;  // プレイヤーオブジェクトの参照
    private bool canAttack = true;  // 攻撃可能かどうかのフラグ

    void Start()
    {
        currentHP = maxHP;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && canAttack)
            {
                StartCoroutine(FireProjectile());
            }
        }
    }

    void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator FireProjectile()
    {
        canAttack = false;
        // 弾丸を生成し、プレイヤーに向かって追尾させる
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(player, projectileSpeed);
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("敵が倒されました");
        // 死亡アニメーションを再生する場合、ここでトリガーする
        Destroy(gameObject);
    }
}