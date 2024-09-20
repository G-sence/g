using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playControl : MonoBehaviour
{
    private GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        bg = GameObject.Find("BG1");
    }

    // Update is called once per frame
    void Update()
    {
        bg.GetComponent<Renderer>().material.SetTextureOffset("_MainTex",new Vector2(Time.time/5 , 0));
    }
}
