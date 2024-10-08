using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshot : MonoBehaviour
{
    public GameObject shellPrefab;
    private int count;

    void Update()
    {
        count += 1;

        if (count % 100 == 0)
        {
            // �e�𔭎˂���
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // �e���͎��R�ɐݒ�
            shellRb.AddForce(transform.forward * 500);

            // 4�b��ɖC�e��j�󂷂�
            Destroy(shell, 4.0f);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Player hit");
        }
    }
}
