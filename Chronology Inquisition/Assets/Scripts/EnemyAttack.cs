using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {
	public float hitCooldown = 0.7f;
	public int hitDamage = 20;
	public AudioSource[] attacks;
	EnemyHealth eHealth;
	PlayerHealth pHealth;
	GameObject player;
	bool playerInHitRange;
	bool isAttacking;
	float timeAfterLastHit;
	int currNum = 0;
	bool canAttack = true;
	Animator ani;

	/// <summary>
	/// Initializes variables.
	/// </summary>
	void Awake(){
		ani = GetComponent<Animator> ();
		player = GameObject.FindWithTag ("Player");
		pHealth = player.GetComponent<PlayerHealth> ();
		eHealth = GetComponent<EnemyHealth> ();
	}


	public bool CanAttack{
		get{ return canAttack; }
		set{ canAttack = value; }
	}


	/// <summary>
	/// If the player is in the enemy trigger it will allow it to attack the player
	/// </summary>
	void OnTriggerEnter(Collider other){
		if (other.isTrigger)
			return;
		if(other.CompareTag("Player")){
			
			playerInHitRange = true;

		}
	}
	/// <summary>
	/// If the player is not in the enemy trigger it will not allow it to attack the player
	/// </summary>
	void OnTriggerExit(Collider other){
		if (other.isTrigger)
			return;
		if(other.CompareTag("Player")){
			playerInHitRange = false;
		}
	}


	/// <summary>
	/// checks if enemy can attack the player, and if the player is dead, and resets the attack boolean.
	/// </summary>

	void Update(){
		timeAfterLastHit += Time.deltaTime;
		if(timeAfterLastHit >= hitCooldown && playerInHitRange && eHealth.currHealth > 0 && canAttack){
			Attack ();
		}
		if (timeAfterLastHit > 0.01f && ani.GetBool("isAttacking")) {
			isAttacking = false;
			ani.SetBool ("isAttacking", isAttacking);
		}
		if (pHealth.currHealth <= 0) {
			NavMeshAgent nav = GetComponent<NavMeshAgent> ();
			EnemyMovement eMove = GetComponent<EnemyMovement> ();
			eMove.enabled = false;
			nav.enabled = false;
		}
	}

	/// <summary>
	/// Resets attack timer and attacks player if the player is alive
	/// </summary>
	void Attack(){
		timeAfterLastHit = 0;
		if (pHealth.currHealth > 0) {
			if (currNum > attacks.Length - 1)
				currNum = 0;
			if (attacks.Length > 0)
				attacks [currNum].Play();
			
			currNum++;
			isAttacking = true;
			ani.SetBool ("isAttacking", isAttacking);
			pHealth.Damage(hitDamage);
		}
	}
}
