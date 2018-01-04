using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camcontrol : MonoBehaviour {
	public float lookSpeedH = 2f;
	public float lookSpeedV = 2f;
	public float zoomSpeed = 2f;
	public float dragSpeed = 6f;

	private float yaw = 0f;
	private float pitch = 0f;

	void Update ()
	{
		//Look around with Right Mouse
		if (Input.GetMouseButton(1))
		{
				yaw += lookSpeedH * Input.GetAxis("Mouse X");
				pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

				transform.eulerAngles = new Vector3(pitch, yaw, 0f);
		}

		//drag camera around with Middle Mouse
		if (Input.GetMouseButton(2))
		{
				transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed,   -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);
		}

		//Zoom in and out with Mouse Wheel
		transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);

		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(new Vector3(dragSpeed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(new Vector3(-dragSpeed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0,-dragSpeed * Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0,dragSpeed * Time.deltaTime,0));
		}
	}
}
