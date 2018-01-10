using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
	public float speed = 10f;
	public float angularSpeed = 360f;

	public AudioSource special;
	private float localTimeScale = 1.0f;
	float timeAfterLastShout = 100f;
	float shoutCooldown = 50f;

	NavMeshAgent nav;
	Transform p; //player
	//Initializes the variables
	
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		p = GameObject.FindWithTag ("Player").transform;
		shoutCooldown += Random.Range (100, 1000);

	}
	public float LocalTimeScale{
		get{return localTimeScale;}
		set{
			float val = value;
			float mp = val/localTimeScale;
			nav.velocity *= mp;
			nav.speed = mp * speed;
			nav.angularSpeed = mp * speed;
		}
	}
	// Sets enemy direction to players current position.
	void FixedUpdate () {
		if (special != null) {
			timeAfterLastShout += Time.deltaTime;
			if (timeAfterLastShout >= shoutCooldown) {
				timeAfterLastShout = 0;
				shoutCooldown = Random.Range (200, 1200);
				special.Play ();
			}
		}

		nav.SetDestination (p.position);
	}
}

