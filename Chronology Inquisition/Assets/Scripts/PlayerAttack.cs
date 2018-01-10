using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public int lightDamage = 10;
	public int heavyDamage = 30;
	public AudioSource lightAttackSound;
	public AudioSource heavyAttackSound;
    Animator anim;
	SphereCollider sphere;
	int lightAttack = -1;
	int heavyAttack = -1;
	int enemiesMask;
	float timeAfterLastHit;
	float timeAfterLastLight;
	float timeAfterLastHeavy;
	float heavyCooldown = 3f;
	float lightCooldown = 0.5f;
	bool isAttacking;
	bool isInCombo;
	bool inHeavyCooldown;
	bool inLightCooldown;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		sphere = GetComponent<SphereCollider> ();
		enemiesMask = LayerMask.GetMask ("Enemies");
	}
	
	// Update is called once per frame
	void Update () {
		timeAfterLastHit += Time.deltaTime;
		timeAfterLastHeavy += Time.deltaTime;
		timeAfterLastLight += Time.deltaTime;
		if (Input.GetMouseButtonDown (0) && !inLightCooldown) {
			AttackLight (sphere);
		} else if (Input.GetMouseButtonDown (1) && !inHeavyCooldown) {
			AttackHeavy (sphere);
		}

		if (timeAfterLastHit > 0.5f && anim.GetBool("isAttacking"))
			anim.SetBool ("isAttacking", isAttacking);
		else if (timeAfterLastHit > 2.0f && isInCombo) {
			lightAttack = -1;
			heavyAttack = -1;
			anim.SetInteger ("lightAttack", lightAttack);
			anim.SetInteger ("heavyAttack", heavyAttack);
			isInCombo = false;
		}
		if (timeAfterLastLight >= lightCooldown && inLightCooldown)
			inLightCooldown = false;
		if (timeAfterLastHeavy >= heavyCooldown && inHeavyCooldown)
			inHeavyCooldown = false;
	}
	
	void AttackLight(SphereCollider sph){
		if (!lightAttackSound.isPlaying)
			lightAttackSound.Play ();
		isAttacking = true;
		timeAfterLastHit = 0f;
		timeAfterLastLight = 0f;
		anim.SetBool ("isAttacking", isAttacking);
		lightAttack += 1;
		heavyAttack = -1;
		inLightCooldown = true;
		if (lightAttack > 2)
			lightAttack = 0;
		anim.SetInteger ("lightAttack", lightAttack);
		anim.SetInteger ("heavyAttack", heavyAttack);
		isAttacking = false;
		isInCombo = true;
		Collider[] col = Physics.OverlapSphere(sph.bounds.center, sph.radius, enemiesMask);
		foreach (Collider enemy in col) {
			EnemyHealth eHealth = enemy.GetComponent<EnemyHealth> ();
			eHealth.TakeDamage (lightDamage);
		}
		
	}
	void AttackHeavy(SphereCollider sph){
		if (!heavyAttackSound.isPlaying)
			heavyAttackSound.Play ();
		isAttacking = true;
		timeAfterLastHit = 0f;
		timeAfterLastHeavy = 0f;
		anim.SetBool ("isAttacking", isAttacking);
		if (lightAttack > 0)
			inLightCooldown = true;
		lightAttack = -1;
		heavyAttack += 1;
		if (heavyAttack > 1){
			heavyAttack = 0;
			inHeavyCooldown = true;
		}
		anim.SetInteger ("lightAttack", lightAttack);
		anim.SetInteger ("heavyAttack", heavyAttack);

		isAttacking = false;
		isInCombo = true;
		Collider[] col = Physics.OverlapSphere(sph.bounds.center, sph.radius, enemiesMask);
		foreach (Collider enemy in col) {
			EnemyHealth eHealth = enemy.GetComponent<EnemyHealth> ();
			eHealth.TakeDamage (lightDamage);
		}
	}
}
