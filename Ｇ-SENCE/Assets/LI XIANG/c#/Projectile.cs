using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private float speed;

    public void SetTarget(GameObject target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject); // ターゲットが存在しない場合は弾丸を破棄する
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().TakeDamage(10);  // プレイヤーにダメージを与える
            Destroy(gameObject);  // 弾丸を破棄する
        }
    }
}