using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 200;
	public int currHealth;
	bool isDead;
	float timeFromLastAttacked;
	float timeFromLastHeal;
	const float HEALDELAY = 5f;
	const float REGENDELAY = 0.1f;
	PlayerMovement pMove;
	Animator ani;
	PlayerAttack pAttack;
	public Image playerHealth;

	/// <summary>
	/// Initializes script variables
	/// </summary>
	void Awake () {
		pMove = GetComponent<PlayerMovement> ();
		currHealth = startingHealth;
		ani = GetComponent<Animator> ();
	}
		
	void LateUpdate(){
		timeFromLastAttacked += Time.deltaTime;
		timeFromLastHeal += Time.deltaTime;
		if (!isDead && timeFromLastAttacked >= HEALDELAY && timeFromLastHeal >= REGENDELAY) {
			timeFromLastHeal = 0f;
			currHealth = Mathf.Clamp (currHealth + 1, 0, 200);
			UpdateHealthBar (currHealth, playerHealth);
		}
	}

	/// <summary>
	/// Damage the player by specified amount
	/// </summary>
	public void Damage(int amount){
		currHealth -= amount;
		currHealth = Mathf.Clamp (currHealth, 0, 200);
		timeFromLastAttacked = 0f;
		UpdateHealthBar (currHealth, playerHealth);
		timeFromLastHeal = 0f;
		if (currHealth <= 0 && !isDead) {
			Dead ();
		}
	}
	void UpdateHealthBar(int health, Image healthBar)
	{
		float ratio = (float)(health) /(float) (startingHealth);
		healthBar.fillAmount = ratio;
	}

	/// <summary>
	/// What to do if player is dead
	/// </summary>
	void Dead(){
		pAttack = GetComponentInChildren<PlayerAttack> ();
		ani.SetTrigger ("isDead");
		pMove.enabled = false;
		pAttack.enabled = false;
		isDead = true;
		SceneManager.LoadScene (0);
	}
		
}
