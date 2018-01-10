using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public Transform focus;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;
	public float zoomSpeed = 0.1f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = 2f;
	public float distanceMax = 10f;

    Rigidbody rb;

	float x = 0.0f;
	float y = 0.0f;
	float distance = 5.0f;
	int cameraModifyableMask;
	void Start () 
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody>();
		cameraModifyableMask = LayerMask.GetMask ("CameraDontTouch");
		if (rb != null)
		{
			rb.freezeRotation = true;
		}
	}
	void FixedUpdate(){
		float change = 0f;
		if (Input.GetKey ("-"))
			change = 1f;
		else if (Input.GetKey ("="))
			change = -1f;
		else if (Input.GetKey (KeyCode.Q) || Input.GetKey (KeyCode.LeftArrow)) 
			x -= 3f;
		else if (Input.GetKey (KeyCode.E) || Input.GetKey (KeyCode.RightArrow))
			x += 3f; 
		distance = Mathf.Clamp(distance + change*zoomSpeed, distanceMin, distanceMax);


	}
	void LateUpdate () 
	{
		Quaternion rotation;
		RaycastHit hit;
		Vector3 negDistance, position;
		Cursor.lockState = CursorLockMode.Locked;
		if (focus) 
		{
			//x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			y = BindAngle(y, yMinLimit, yMaxLimit);

			rotation = Quaternion.Euler(y, x, 0);

			if (Physics.Linecast (focus.position, transform.position,out hit, cameraModifyableMask)) 
			{
				distance -=  hit.distance;
			}

		    negDistance = new Vector3(0.0f, 0.0f, -distance);
			position = rotation * negDistance + focus.position;

			transform.rotation = rotation;
			transform.position = position;
		}
	}

	public static float BindAngle(float angle, float min, float max)
	{
		if (angle < -360F)     angle += 360F;
		else if (angle > 360F) angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}
