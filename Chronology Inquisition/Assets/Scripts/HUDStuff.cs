using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDStuff : MonoBehaviour {

    public float playerHealth = 200f;
    public float playerSpecial = 0f;
    public float gundeezedHealth = 200f;
    public float timegodHealth = 200f;

    public float maxHealth = 200f;
    public float maxSpecial = 200f;

    public Image playerBar;
    public Image playerSpecialBar;
    public Image gundeezedBar;
    public Image timegodBar;

	// Use this for initialization
	void Start() {
        UpdateHealthBar(playerHealth, playerBar);
        UpdateHealthBar(gundeezedHealth, gundeezedBar);
        UpdateHealthBar(timegodHealth, timegodBar);
        UpdateSpecialBar(playerSpecial, playerSpecialBar);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G))
        {
            gundeezedHealth -= 50f;
            playerSpecial += 50f;
            if (gundeezedHealth < 0)
            {
                gundeezedHealth = 0;
                Debug.Log("Dead");
            }
            Debug.Log("Max Health is: " + maxHealth + ", Health is " + gundeezedHealth);
            UpdateHealthBar(gundeezedHealth, gundeezedBar);
            UpdateSpecialBar(playerSpecial, playerSpecialBar);
        }
        else if(Input.GetKeyDown(KeyCode.H))
        {
            timegodHealth -= 50f;
            playerSpecial += 50f;
            if (timegodHealth < 0)
            {
                timegodHealth = 0;
                Debug.Log("Dead");
            }
            UpdateHealthBar(timegodHealth, timegodBar);
            UpdateSpecialBar(playerSpecial, playerSpecialBar);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            playerHealth -= 50f;
            playerSpecial += 50f;
            if (playerHealth < 0)
            {
                playerHealth = 0;
                Debug.Log("Dead");
            }
            UpdateHealthBar(playerHealth, playerBar);
            UpdateSpecialBar(playerSpecial, playerSpecialBar);
        }
    }

    private void UpdateHealthBar(float health, Image healthBar)
    {
        float ratio = health / maxHealth;
        healthBar.fillAmount = ratio;
    }

    private void UpdateSpecialBar(float special, Image specialBar)
    {
        float ratio = special / maxSpecial;
        specialBar.fillAmount = ratio;
    }
}
