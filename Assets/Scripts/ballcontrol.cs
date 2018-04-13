using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ballcontrol : MonoBehaviour
{

    //public Transform clubObj;
    private float zForce = 100;
    public Transform arrowObj;
    //	private bool sandtrapped = false;
    //	public Transform sandsprayObj;
    private bool isJumping = false;
    //	public Transform portalOutObj;
    private float speed;
    private bool isMoving = false;
    public GameObject message;
    AudioSource audio1, audio2;
    private bool isStrikable = true;
    public GameObject resurrect;
    public GameObject fireworks;


    // Use this for initialization
    void Start()
    {
        AudioSource[] aSources = GetComponents<AudioSource>();
        audio1 = aSources[0];
        audio2 = aSources[1];
        isStrikable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isStrikable)
                Strike();
        }

        if ((Input.GetKeyDown("space")) && (!isJumping))
        {
            GetComponent<Rigidbody>().velocity = new Vector3((GetComponent<Rigidbody>().velocity.x), 3, (GetComponent<Rigidbody>().velocity.z));
            isJumping = true;
        }

        if (Input.GetKey("q"))
        {
            transform.Rotate(0, -1, 0);
        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(0, 1, 0);
        }

        if (Input.GetKey("z"))
        {
            zForce += 10;
        }

        if (Input.GetKey("s"))
        {
            zForce -= 10;
        }

        if (zForce < 20)
        {
            zForce = 20;
        }

        if (zForce > 1000)
        {
            zForce = 1000;
        }

        if (isMoving == true)
        {
            speed = GetComponent<Rigidbody>().velocity.magnitude;
            if (speed < 0.08 && speed > 0.001f)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                isMoving = false;
                isStrikable = true;
                NewTurn();
            }
        }

        if (GetComponent<Rigidbody>().velocity.y < -20)
        {
            ReplaceBall();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "cup")
        {
            Debug.Log("Completed!");
            message.GetComponent<UnityEngine.UI.Text>().text = "Victory !";
            message.GetComponent<UnityEngine.UI.Text>().enabled = true;
            audio2.Play();
            Destroy(Instantiate(fireworks, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(-90, 0, 0)), 3);
            gameflow.currentStrokes = 0;
            StartCoroutine(delayLoad());
        }
        //		if (other.name == "sandtrap") 
        //		{
        //			sandtrapped = true;
        //			GetComponent<Rigidbody> ().drag = 4;
        //		}
        //		if (other.name == "portalIn") 
        //		{
        //			transform.position = portalOutObj.GetComponent<Transform> ().position;
        //			GetComponent<Rigidbody> ().velocity = new Vector3 (-(GetComponent<Rigidbody> ().velocity.z), 0, 0);
        //		}
    }

    //	void OnTriggerExit(Collider another) 
    //	{
    //		if (another.name == "sandtrap") 
    //		{
    //			sandtrapped = false;
    //			GetComponent<Rigidbody> ().drag = 0;
    //		}
    //	}

    IEnumerator delayLoad()
    {
        yield return new WaitForSeconds(5);
        message.GetComponent<UnityEngine.UI.Text>().enabled = false;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Randomised":
                SceneManager.LoadScene("Randomised");
                break;
            //		case "hole2":
            //			SceneManager.LoadScene ("hole3");
            //			break;
            //		case "hole3":
            //			SceneManager.LoadScene ("hole4");
            //			break;
            //		case "hole4":
            //			SceneManager.LoadScene ("hole5");
            //			break;
            //		case "hole5":
            //			SceneManager.LoadScene ("hole6");
            //			break;
            //		case "hole6":
            //			SceneManager.LoadScene ("hole7");
            //			break;
            //		case "hole7":
            //			SceneManager.LoadScene ("hole8");
            //			break;
            default:
                SceneManager.LoadScene("test1");
                break;
        }
    }

    void Strike()
    {
        gameflow.currentStrokes++;
        gameflow.totalStrokes++;

        GetComponent<Rigidbody>().AddRelativeForce(0, 0.01f, zForce);
        //		if (sandtrapped)
        //			Instantiate (sandsprayObj, transform.position, sandsprayObj.rotation);

        isMoving = true;
        isStrikable = false;

        audio1.Play();
        arrowObj.GetComponent<SpriteRenderer>().enabled = false;
        arrowObj.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    void NewTurn()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Transform>().eulerAngles = Vector3.zero;
        arrowObj.GetComponent<Transform>().position = transform.position;
        arrowObj.GetComponent<Transform>().eulerAngles = new Vector3(90, 0, 0);
        isJumping = false;
        arrowObj.GetComponent<SpriteRenderer>().enabled = true;
        arrowObj.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    void ReplaceBall()
    {
        gameflow.currentStrokes += 2;
        gameflow.totalStrokes += 2;
        transform.position = new Vector3(0, 0.3f, 0);
        Destroy(Instantiate(resurrect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(-90, 0, 0)), 3);
        NewTurn();
    }
}
