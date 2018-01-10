using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
	public int currHealth;
	public int startingHealth = 100;
	public int abilityValue = 5;
	public AudioSource death;
	public GameObject healthBar;
	public bool isBoss = false;
	public Image bossBar;
	float damageMultiplier = 1.0f;
	Animator ani;
	CapsuleCollider capCollider;
	EnemyMovement eMove;
    NavMeshAgent nav;
	Spawner spawn;
	PlayerAbility pA;
	NPCDialog npc;
	bool isDead;
	float timeSinceLastAbilityHurt;

	// Initializes variables and generates enemy health for the round.
	void Start () {
		ani = GetComponent<Animator> ();
		capCollider = GetComponent<CapsuleCollider> ();
		eMove = GetComponent<EnemyMovement> ();
		nav = GetComponent<NavMeshAgent> ();
		if (!isBoss)
			spawn = GameObject.FindWithTag ("GameManager").GetComponent<Spawner> ();
		else {
			npc = GameObject.FindWithTag ("GameManager").GetComponent<NPCDialog> ();
		}
		pA = GameObject.FindWithTag ("Player").GetComponent<PlayerAbility> ();
		currHealth = startingHealth;
	}

	//Deal specific damage to enemy.
	public void TakeDamage(int damage){
		float barHealth = 0f;
		if (isDead)
			return;
		if (isBoss)
			pA.PowerBar += abilityValue;
		currHealth -= (int)(damage * damageMultiplier);
		barHealth = Mathf.Clamp ((float)(currHealth) / (float)(startingHealth), 0, startingHealth);
		if (!isBoss)
			UpdateHealthBar (barHealth);
		else
			UpdateHealthBar (currHealth, bossBar);
		if (currHealth <= 0)
			Death ();
	}

	public int CurrentHealth {
		get{ return currHealth; }
		set{ currHealth = value ;}
	}
	public float DamageMultiplier{
		get{ return damageMultiplier; }
		set{ damageMultiplier = value;}
	}

	void LateUpdate(){
		if (PlayerAbility.isTimeHurt) {
			timeSinceLastAbilityHurt += Time.deltaTime;
			if (timeSinceLastAbilityHurt >= 0.2f){
				TakeDamage (1);
				timeSinceLastAbilityHurt = 0;
			}
		}
	}

	/// <summary>
	/// What to do if enemy is dead.
	/// </summary>
	void Death(){
		isDead = true;
		eMove.enabled = false;
		nav.enabled = false;
		death.Play ();
		capCollider.isTrigger = true;
		ani.SetTrigger ("isDead");
		gameObject.layer = 0;
		if (!isBoss) {
			spawn.currentEnemies -= 1;
			spawn.KilledEnemies += 1;
		} else {
			npc.IsEnding = true;
		}
		pA.PowerBar += abilityValue;
		pA.PowerBar = Mathf.Clamp (pA.PowerBar, 0, 100);
		Destroy (gameObject, 2f);
	}

	void UpdateHealthBar(float health){
		healthBar.transform.localScale = new Vector3 (health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
	void UpdateHealthBar(float health, Image healthBar){
		float ratio = health / startingHealth;
		healthBar.fillAmount = ratio;
	}

}
