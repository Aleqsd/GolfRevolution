using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow2con : MonoBehaviour
{

    public float forceColor = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("z"))
        {
            forceColor -= 0.02f;
            GetComponent<SpriteRenderer>().color = new Color(1, forceColor, forceColor);
        }

        if (Input.GetKey("s"))
        {
            forceColor += 0.02f;
            GetComponent<SpriteRenderer>().color = new Color(1, forceColor, forceColor);
        }
    }
}
