using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	float x = 0.0f;
	float y = 0.0f;
	public float moveSpeed = 10f;
	Animator anim;
	Vector3 movement;

	Rigidbody rb;
	// Use this for initialization
	void Awake () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horiz = Input.GetAxisRaw ("Horizontal");
		float vert = Input.GetAxisRaw ("Vertical");

		Move (horiz,vert);
		Rotate ();
		CheckMovement (horiz, vert);

	}
	void Move(float horizMovement, float vertMovement){
		Transform newPlayerPos = transform;
		
		movement.Set (horizMovement , 0.0f, vertMovement );

		movement = movement.normalized * moveSpeed * Time.deltaTime;

		newPlayerPos.localPosition += movement.x * transform.right;
		newPlayerPos.localPosition += movement.z * transform.forward;
		rb.MovePosition (newPlayerPos.position);
	}
	void Rotate(){
		Quaternion rotation;

		if (Input.GetKey (KeyCode.Q))
			x -= 3f;
		else if (Input.GetKey (KeyCode.E))
			x += 3f;
		rotation = Quaternion.Euler (y, x, 0);
		rb.MoveRotation(rotation);
	}
	void CheckMovement(float horizontalMovement, float verticalMovement){
		bool isMoving = false;
		if (horizontalMovement != 0f || verticalMovement != 0f)
			isMoving = true;
		anim.SetBool ("isWalking", isMoving);
	}
}
