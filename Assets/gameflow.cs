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
	public static float par = 0;

	public GameObject land;
	public GameObject side;
	public GameObject straight;
	public GameObject corner;
	public GameObject end;
	public GameObject hole;
	public GameObject windmill;
	public GameObject castle;
	public GameObject gap;
	public GameObject tunnel;
	public GameObject ramp;
	public GameObject narrow;

	public static int size;
	public int sizeMax = 100;
	//private List<GameObject> map;
	private List<Vector3> positions; // tuples unavailable

	// Use this for initialization
	void Start ()
	{
		//map = new List<GameObject> ();
		positions = new List<Vector3> ();
		AddLandPosition (Vector3.zero);
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void AddLandPosition (Vector3 vector)
	{
		positions.Add (vector);
	}

	void GenerateLandPositions()
	{
		size = Random.Range (5, sizeMax);
		for (int i = 0; i < size; i++) {
			//Vector3 currentPosition = map [i].transform.position;
			Vector3 currentPosition = positions[i];
			Direction currentDirection = (Direction)Random.Range (0, 3);

			if (currentDirection.Equals (Direction.North)) {
				Vector3 newPosition = new Vector3 (currentPosition.x, currentPosition.y, currentPosition.z + 3);
				if (positions.Contains (newPosition)) {
					i--;
				} else
					AddLandPosition (newPosition);
			} else if (currentDirection.Equals (Direction.South)) {
				Vector3 newPosition = new Vector3 (currentPosition.x, currentPosition.y, currentPosition.z - 3);
				if (positions.Contains (newPosition)) {
					i--;
				} else
					AddLandPosition (newPosition);
			} else if (currentDirection.Equals (Direction.West)) {
				Vector3 newPosition = new Vector3 (currentPosition.x - 3, currentPosition.y, currentPosition.z);
				if (positions.Contains (newPosition)) {
					i--;
				} else
					AddLandPosition (newPosition);
			} else if (currentDirection.Equals (Direction.East)) {
				Vector3 newPosition = new Vector3 (currentPosition.x + 3, currentPosition.y, currentPosition.z);
				if (positions.Contains (newPosition)) {
					i--;
				} else
					AddLandPosition (newPosition);
			}
		}
		Debug.Log ("Lands count : " + positions.Count);
		par = positions.Count / 2;
	}

	void GenerateLands()
	{
		Vector3 last = positions [positions.Count - 1];
		bool isWindmill = false;
		bool isCastle = false;
		bool isGap = false;
		bool isTunnel = false;
		bool isRamp = false;
		bool isNarrow = false;

		foreach (Vector3 pos in positions) {
			int walls = 0;
			bool northWall = false;
			bool southWall = false;
			bool westWall = false;
			bool eastWall = false;
			Vector3 checkNorth = new Vector3 (pos.x, pos.y, pos.z + 3);
			Vector3 checkSouth = new Vector3 (pos.x, pos.y, pos.z - 3);
			Vector3 checkWest = new Vector3 (pos.x-3, pos.y, pos.z);
			Vector3 checkEast = new Vector3 (pos.x+3, pos.y, pos.z);
			GameObject currentGO = new GameObject ();

			if (!positions.Contains (checkNorth)) {
				walls++;
				northWall = true;
			}
			if (!positions.Contains (checkSouth)) {
				walls++;
				southWall = true;
			}
			if (!positions.Contains (checkWest)) {
				walls++;
				westWall = true;
			}
			if (!positions.Contains (checkEast)) {
				walls++;
				eastWall = true;
			}

			if (walls == 1) 
			{
				currentGO = Instantiate (side);
				currentGO.transform.position = pos;
				if (northWall)
					currentGO.transform.Rotate (0, 270, 0);
				else if (southWall)
					currentGO.transform.Rotate (0, 90, 0);
				else if (westWall)
					currentGO.transform.Rotate (0, 180, 0);
			}
			else if (walls == 2) 
			{
				if (northWall && southWall) 
				{
					if (!isGap) {
						currentGO = Instantiate (gap);
						currentGO.transform.position = pos;
						currentGO.transform.Rotate (0, 90, 0);
						isGap = true;
					} else if (!isNarrow) {
						currentGO = Instantiate (narrow);
						currentGO.transform.position = pos;
						currentGO.transform.Rotate (0, 90, 0);
						isNarrow = true;
					} else if (!isTunnel) {
						currentGO = Instantiate (tunnel);
						currentGO.transform.position = pos;
						currentGO.transform.Rotate (0, 90, 0);
						isTunnel = true;
					} else {
						currentGO = Instantiate (straight);
						currentGO.transform.position = pos;
						currentGO.transform.Rotate (0, 90, 0);
					}
				}
				else if (northWall && westWall) 
				{
					currentGO = Instantiate (corner);
					currentGO.transform.position = pos;
					currentGO.transform.Rotate (0, 180, 0);
				}
				else if (northWall && eastWall) 
				{
					currentGO = Instantiate (corner);
					currentGO.transform.position = pos;
					currentGO.transform.Rotate (0, 270, 0);
				}
				else if (eastWall && southWall) 
				{
					currentGO = Instantiate (corner);
					currentGO.transform.position = pos;
				}
				else if (eastWall && westWall) 
				{
					if (!isWindmill) 
					{
						currentGO = Instantiate (windmill);
						currentGO.transform.position = pos;
						isWindmill = true;
					}
					else if (!isCastle) 
					{
						currentGO = Instantiate (castle);
						currentGO.transform.position = pos;
						isCastle = true;
					}
					else if (!isRamp) 
					{
						currentGO = Instantiate (ramp);
						currentGO.transform.position = pos;
						isRamp = true;
					}
					else 
					{
						currentGO = Instantiate (straight);
						currentGO.transform.position = pos;
					}
				}
				else if (westWall && southWall) 
				{
					currentGO = Instantiate (corner);
					currentGO.transform.position = pos;
					currentGO.transform.Rotate (0, 90, 0);
				}
			}
			else if (walls == 3) 
			{
				if (pos.Equals (last)) 
					currentGO = Instantiate (hole);
				else 
					currentGO = Instantiate (end);
				if (!northWall) 
				{
					currentGO.transform.position = pos;
				} 
				else if (!westWall) 
				{
					currentGO.transform.position = pos;
					currentGO.transform.Rotate (0, 270, 0);
				}
				else if (!eastWall) 
				{
					currentGO.transform.position = pos;
					currentGO.transform.Rotate (0, 90, 0);
				} 
				else if (!southWall) 
				{
					currentGO.transform.position = pos;
					currentGO.transform.Rotate (0, 180, 0);
				} 
			}
			else 
			{
				currentGO = Instantiate (land);
				currentGO.transform.position = pos;
			}
		}
	}

	void GenerateMap ()
	{
		GenerateLandPositions ();
		GenerateLands ();
	}
}
