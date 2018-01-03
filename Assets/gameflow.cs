using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameflow : MonoBehaviour
{

	enum Direction
	{
		North,
		South,
		West,
		East}

	;

	public static float totalStrokes = 0;
	public static float currentStrokes = 0;

	public GameObject land;
	public int size;
	public List<GameObject> map;

	// Use this for initialization
	void Start ()
	{
		map = new List<GameObject> ();
		CreateLand (0, 0, 0);
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void CreateLand (float x, float y, float z)
	{
		GameObject creation = Instantiate (land);
		creation.transform.position = new Vector3 (x, y, z);
		map.Add (creation);
	}

	void GenerateMap ()
	{
		size = Random.Range (5, 20);
		for (int i = 0; i < size; i++) {
			Vector3 currentPosition = map [i].transform.position;
			Direction currentDirection = (Direction)Random.Range (0, 3);

			switch (currentDirection) {
			case Direction.North:
				CreateLand (currentPosition.x, currentPosition.y, currentPosition.z + 3);
				break;
			case Direction.South:
				CreateLand (currentPosition.x, currentPosition.y, currentPosition.z - 3);
				break;
			case Direction.West:
				CreateLand (currentPosition.x - 3, currentPosition.y, currentPosition.z);
				break;
			case Direction.East:
				CreateLand (currentPosition.x + 3, currentPosition.y, currentPosition.z);
				break;
			}
		}
	}
}
