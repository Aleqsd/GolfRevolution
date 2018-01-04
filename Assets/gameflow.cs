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
		East
	};

	public static float totalStrokes = 0;
	public static float currentStrokes = 0;

	public GameObject land;
	public GameObject wall;
	private int size;
	private List<GameObject> map;
	private List<Vector3> positions; // tuples unavailable

	// Use this for initialization
	void Start ()
	{
		map = new List<GameObject> ();
		positions = new List<Vector3> ();
		CreateLand (new Vector3(0,0,0));
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void CreateLand (Vector3 vector)
	{
		GameObject creation = Instantiate (land);
		creation.transform.position = vector;
		map.Add (creation);
		positions.Add (vector);
	}

	void CreateWall (Vector3 position, Direction direction)
	{
		GameObject creation = Instantiate (wall);

		if (direction.Equals (Direction.North)) {
			creation.transform.position = new Vector3(position.x, position.y +0.1f, position.z+1.42f);
			creation.transform.Rotate (0, 90, 0);
		}
		else if (direction.Equals (Direction.South)) {
			creation.transform.position = new Vector3(position.x, position.y +0.1f, position.z-1.42f);
			creation.transform.Rotate (0, 90, 0);
		}
		else if (direction.Equals (Direction.West)) {
			creation.transform.position = new Vector3(position.x-1.42f, position.y +0.1f, position.z);
		}
		else if (direction.Equals (Direction.East)) {
			creation.transform.position = new Vector3(position.x+1.42f, position.y +0.1f, position.z);
		}
	}

	void GenerateLands()
	{
		size = Random.Range (5, 20);
		for (int i = 0; i < size; i++) {
			Vector3 currentPosition = map [i].transform.position;
			Direction currentDirection = (Direction)Random.Range (0, 3);

			if (currentDirection.Equals (Direction.North)) {
				Vector3 newPosition = new Vector3 (currentPosition.x, currentPosition.y, currentPosition.z + 3);
				if (positions.Contains (newPosition)) {
					i--;
				} else
					CreateLand (newPosition);
			} else if (currentDirection.Equals (Direction.South)) {
				Vector3 newPosition = new Vector3 (currentPosition.x, currentPosition.y, currentPosition.z - 3);
				if (positions.Contains (newPosition)) {
					i--;
				} else
					CreateLand (newPosition);
			} else if (currentDirection.Equals (Direction.West)) {
				Vector3 newPosition = new Vector3 (currentPosition.x - 3, currentPosition.y, currentPosition.z);
				if (positions.Contains (newPosition)) {
					i--;
				} else
					CreateLand (newPosition);
			} else if (currentDirection.Equals (Direction.East)) {
				Vector3 newPosition = new Vector3 (currentPosition.x + 3, currentPosition.y, currentPosition.z);
				if (positions.Contains (newPosition)) {
					i--;
				} else
					CreateLand (newPosition);
			}
		}
	}

	void GenerateWalls()
	{
		foreach (Vector3 position in positions) {
			Vector3 checkNorth = new Vector3 (position.x, position.y, position.z + 3);
			Vector3 checkSouth = new Vector3 (position.x, position.y, position.z - 3);
			Vector3 checkWest = new Vector3 (position.x-3, position.y, position.z);
			Vector3 checkEast = new Vector3 (position.x+3, position.y, position.z);

			if (!positions.Contains (checkNorth)) {
				CreateWall (position,Direction.North);
			}
			if (!positions.Contains (checkSouth)) {
				CreateWall (position,Direction.South);
			}
			if (!positions.Contains (checkWest)) {
				CreateWall (position,Direction.West);
			}
			if (!positions.Contains (checkEast)) {
				CreateWall (position,Direction.East);
			}
		}
	}

	void GenerateMap ()
	{
		GenerateLands ();
		GenerateWalls ();
	}
}
