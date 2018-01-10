using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {
	EnemyHealth eHealth;

	bool isRegening, stageOne = true, stageTwo, stageThree;
	const int STAGEONEHEALTH = 400, STAGETWOHEALTH = 380, STAGETHREEHEALTH = 350;

	// Use this for initialization
	void Start () {
		eHealth = GetComponent<EnemyHealth> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (eHealth.CurrentHealth <= 350 && stageOne || eHealth.CurrentHealth <= 250 && stageTwo || eHealth.CurrentHealth <= 150 && stageThree)
			isRegening = true;	
		if (isRegening && ((eHealth.currHealth < STAGEONEHEALTH && stageOne) || (eHealth.currHealth < STAGETWOHEALTH && stageTwo) || 
			(eHealth.currHealth < STAGETHREEHEALTH && stageThree)))
				eHealth.CurrentHealth += 1;
		if (isRegening && eHealth.currHealth > STAGEONEHEALTH && stageOne) {
			stageOne = false;
			stageTwo = true;
			eHealth.DamageMultiplier = 1.5f;
		} else if (isRegening && eHealth.currHealth > STAGETWOHEALTH && stageTwo) {
			stageTwo = false;
			stageThree = true;
			eHealth.DamageMultiplier = 2f;
		} else if (isRegening && eHealth.currHealth > STAGETHREEHEALTH && stageThree) {
			stageThree = false;
			eHealth.DamageMultiplier = 2.5f;
		}

	}
}
