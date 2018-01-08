using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateclub : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("q"))
        {
            transform.Rotate(0, 1, 0);
        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(0, -1, 0);
        }
    }
}
