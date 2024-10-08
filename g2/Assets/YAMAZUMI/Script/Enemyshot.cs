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
            // ’e‚ğ”­Ë‚·‚é
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // ’e‘¬‚Í©—R‚Éİ’è
            shellRb.AddForce(transform.forward * 500);

            // 4•bŒã‚É–C’e‚ğ”j‰ó‚·‚é
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
