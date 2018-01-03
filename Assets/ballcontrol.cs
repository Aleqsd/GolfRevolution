using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ballcontrol : MonoBehaviour {

    //public Transform clubObj;
	public float zForce = 100;
	public Transform arrowObj;
	public bool sandtrapped = false;
	public Transform sandsprayObj;
	public bool isJumping = false;
	public Transform portalOutObj;


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Fire1")) 
		{
			gameflow.currentStrokes++;
			gameflow.totalStrokes++;
			GetComponent<Rigidbody> ().AddRelativeForce (0, 0, zForce);
			if (sandtrapped)
				Instantiate (sandsprayObj, transform.position, sandsprayObj.rotation);

			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
		}

		if ((Input.GetButtonDown ("Fire2")) && (!isJumping)) 
		{
			GetComponent<Rigidbody>().velocity = new Vector3 ((GetComponent<Rigidbody>().velocity.x), 3, (GetComponent<Rigidbody>().velocity.z));
			isJumping = true;
		}

        if(Input.GetKeyDown("space")) 
		{
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GetComponent<Transform> ().eulerAngles = Vector3.zero;
			arrowObj.GetComponent<Transform> ().position = transform.position;
			isJumping = false;
        }

		if (Input.GetKey ("q")) 
		{
			transform.Rotate (0, -1, 0);
		}

		if (Input.GetKey ("d")) 
		{
			transform.Rotate (0, 1, 0);
		}

		if (Input.GetKey ("z")) 
		{
			zForce += 10;
		}

		if (Input.GetKey ("s")) 
		{
			zForce -= 10;
		}

		if (zForce < 20) 
		{
			zForce = 20;
		}

		if (zForce > 1200) 
		{
			zForce = 1200;
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.name == "cup") 
		{
			Debug.Log ("Completed!");
			gameflow.currentStrokes = 0;
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			StartCoroutine (delayLoad ());
		}
		if (other.name == "sandtrap") 
		{
			sandtrapped = true;
			GetComponent<Rigidbody> ().drag = 4;
		}
		if (other.name == "portalIn") 
		{
			transform.position = portalOutObj.GetComponent<Transform> ().position;
			GetComponent<Rigidbody> ().velocity = new Vector3 (-(GetComponent<Rigidbody> ().velocity.z), 0, 0);
		}
	}

	void OnTriggerExit(Collider another) 
	{
		if (another.name == "sandtrap") 
		{
			sandtrapped = false;
			GetComponent<Rigidbody> ().drag = 0;
		}
	}

	IEnumerator delayLoad()
	{
		yield return new WaitForSeconds (2);
		switch (SceneManager.GetActiveScene ().name) 
		{
		case "hole1":
			SceneManager.LoadScene ("hole2");
			break;
		case "hole2":
			SceneManager.LoadScene ("hole3");
			break;
		case "hole3":
			SceneManager.LoadScene ("hole4");
			break;
		case "hole4":
			SceneManager.LoadScene ("hole5");
			break;
		case "hole5":
			SceneManager.LoadScene ("hole6");
			break;
		case "hole6":
			SceneManager.LoadScene ("hole7");
			break;
		case "hole7":
			SceneManager.LoadScene ("hole8");
			break;
		default :
			SceneManager.LoadScene ("hole1");
			break;
		}
	}
}
