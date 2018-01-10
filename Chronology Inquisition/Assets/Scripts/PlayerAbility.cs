using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAbility : MonoBehaviour {
	public static bool isTimeHurt;
	public Image powerImage;
	Collider[] slowedDown;
	const int SLOWCOST = 50;
	const int SPEEDCOST = 100;
	const int MAXPOWER = 100;
	bool inSlowCooldown;
	bool inSpeedCooldown;
	bool inSlow;
	float timeSinceAbility;
	float slowAbilityCooldown = 120f;
	float speedAbilityCooldown = 500f;
	
	int powerBar = 0;
	float slowDuration = 15f;
	float speedDuration = 20f;
	int enemiesMask;

	BoxCollider box;
	// Use this for initialization
	void Start () {
		box = GetComponent<BoxCollider> ();
		enemiesMask = LayerMask.GetMask ("Enemies");
		UpdatePowerBar (powerBar, powerImage);
	}

	public int PowerBar
	{
		get{ return powerBar; }
		set{ 
			powerBar = value; 
			powerBar = Mathf.Clamp (powerBar, 0, MAXPOWER);
			UpdatePowerBar (powerBar, powerImage);
		}
	}

	void UpdatePowerBar(int special, Image powerImg)
	{
		float ratio = (float)(special) /(float) (MAXPOWER);
		powerImg.fillAmount = ratio;
	}

	// Update is called once per frame
	void FixedUpdate () {
		EnemyAttack eAttack;
		EnemyMovement eMove;
		Animator eAnimator;
		timeSinceAbility += Time.deltaTime;
		if (inSlow && timeSinceAbility >= slowDuration) {
			inSlow = false;
			foreach (Collider enemy in slowedDown) {
				if (enemy == null)
					continue;
				eMove = enemy.GetComponent<EnemyMovement> ();
				eAttack = enemy.GetComponent<EnemyAttack> ();
				eAnimator = enemy.GetComponent<Animator> ();
				eAnimator.speed = 1.0f;
				eAttack.CanAttack = true;
				eMove.LocalTimeScale = 1.0f;
			}
		} else if (isTimeHurt && timeSinceAbility >= speedDuration) {
			isTimeHurt = false;
		}
		if (inSlowCooldown && timeSinceAbility < slowAbilityCooldown || inSpeedCooldown && timeSinceAbility < speedAbilityCooldown)
			return;
		if (inSlowCooldown) {
			inSlowCooldown = false;
		} else if (inSpeedCooldown) {
			inSpeedCooldown = false;
		}

		inSpeedCooldown = false;
		if (Input.GetKeyDown (KeyCode.R) && powerBar >= SLOWCOST) {
			inSlowCooldown = true;
			inSlow = true;
			timeSinceAbility = 0f;
			PowerBar -= SLOWCOST;
			slowedDown = Physics.OverlapBox(box.bounds.center, box.bounds.extents, box.transform.rotation, enemiesMask);
			foreach (Collider enemy in slowedDown) {
			    eMove = enemy.GetComponent<EnemyMovement> ();
			    eAttack = enemy.GetComponent<EnemyAttack> ();
				eAnimator = enemy.GetComponent<Animator> ();
				eAnimator.speed = 0.1f;
				eAttack.CanAttack = false;
				eMove.LocalTimeScale = 0.1f;
			}
		}else if(Input.GetKeyDown(KeyCode.F) && powerBar >= SPEEDCOST){
			isTimeHurt = true;
			inSpeedCooldown = true;
			timeSinceAbility = 0f;
			PowerBar -= SPEEDCOST;
		}
	}
}
