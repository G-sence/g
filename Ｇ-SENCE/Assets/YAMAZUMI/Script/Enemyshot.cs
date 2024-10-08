using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshot : MonoBehaviour
{
    public PlayerMove playermove;
    public GameObject shellPrefab;
    private int count;

    void Update()
    {
        count += 1;

        if (count % 100 == 0)
        {
            // 弾を発射する
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // 弾をプレイヤーに向けて飛ばす
            shellRb.AddForce(transform.forward * 500);

            // 4秒後に弾を破壊する
            Destroy(shell, 4.0f);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playermove.TakeDamage(1);
            Debug.Log("Player hit");
        }
    }
}