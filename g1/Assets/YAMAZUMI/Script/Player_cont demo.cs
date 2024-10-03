using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_contdemo : MonoBehaviour
{
    public Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += 0.01f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= 0.01f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += 0.01f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= 0.01f;
        }
        transform.position = pos;
    }
}
