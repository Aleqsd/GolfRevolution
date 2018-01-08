using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springcon : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
        StartCoroutine(stopSpring());

    }

    IEnumerator stopSpring()
    {
        yield return new WaitForSeconds(.2f);

        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(.80f);

        transform.position = new Vector3(0, -0.57f, 6.97f);
    }
}
