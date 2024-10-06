using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_contdemo : MonoBehaviour
{
    public Vector3 pos;
    public float speed = 3.0f;

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
}
