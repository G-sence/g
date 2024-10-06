using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_contdemo : MonoBehaviour
{
    public Vector3 pos;
    public float speed = 3.0f;

    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public GameObject stage4;
    public GameObject stage5;
    public GameObject stage6;

    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos.x += 0.01f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += speed * transform.up * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= speed * transform.up * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += speed * transform.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= speed * transform.right * Time.deltaTime;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "sencer")
        {
            pos = this.transform.position;

            Debug.Log("hit");

            int x = Random.Range(1, 7);
            int num = x;

            switch (num)
            {
                case 1:
                    Instantiate(stage1, new Vector3(pos.x + 30f, 5, 0.0f), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(stage2, new Vector3(pos.x + 30f, 5.0f, 0.0f), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(stage3, new Vector3(pos.x + 30f, 5.0f, 0.0f), Quaternion.identity);
                    break;
                case 4:
                    Instantiate(stage4, new Vector3(pos.x + 30f, 5.0f, 0.0f), Quaternion.identity);
                    break;
                case 5:
                    Instantiate(stage5, new Vector3(pos.x + 30f, 5.0f, 0.0f), Quaternion.identity);
                    break;
                case 6:
                    Instantiate(stage6, new Vector3(pos.x + 30f, 5.0f, 0.0f), Quaternion.identity);
                    break;

            }

        }
    }
}
