using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage : MonoBehaviour
{
    public GameObject stage01;
    public Vector3 pos;
    private bool make;

    void Start()
    {
        Instantiate(stage01, new Vector3(0f, 5.5f, 0), Quaternion.identity);
        Instantiate(stage01, new Vector3(20f, 5.5f, 0), Quaternion.identity);
        pos = this.transform.position;
        make = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && make == false)
        {
           Instantiate(stage01, new Vector3(pos.x + 20, 5.5f, 0), Quaternion.identity);
            make = true;
        }
    }
}
