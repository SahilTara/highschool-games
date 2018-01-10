using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Spawner : MonoBehaviour {
	public float spawnDelay = 3f;
	public float spawnRate = 5f;
	public int maxEnemies = 24;
	public int currentEnemies = 0;
	public Text enemiesRemaining;

	public int levelEnemies = 30;
	public PlayerHealth pH;
	public GameObject enemy;
	public Transform[] spawnLocations;
	NPCDialog npc;
	//Animator anim;

	int spawnedEnemies = 0;
	int killedEnemies = 0;



	public int KilledEnemies{
		get{ return killedEnemies;}
		set{
			killedEnemies = value; 
			UpdateEnemies ("ENEMIES REMAINING: " + (levelEnemies - killedEnemies), enemiesRemaining);
		}
	}
	void Start(){
		InvokeRepeating ("Spawn", spawnDelay, spawnRate);
		UpdateEnemies ("ENEMIES REMAINING: " + (levelEnemies - killedEnemies), enemiesRemaining);
		npc = GetComponent<NPCDialog> ();
	}
	//If the enemies can be spawned it puts them on the map randomly, if all enemies have been spawned and killed the level is over.
	void Spawn(){
		if (pH.currHealth <= 0)
			return;
		if (spawnedEnemies < levelEnemies && currentEnemies < maxEnemies) {
			spawnedEnemies += 1;
			currentEnemies += 1;
			int i = Random.Range(0, spawnLocations.Length -1);
			Instantiate (enemy, spawnLocations[i].position, spawnLocations[i].rotation);
		} else if(spawnedEnemies == levelEnemies && currentEnemies == 0) {
			npc.IsEnding = true;
		}
	}

	void UpdateEnemies(string content, Text enemies){
		enemies.text = content;
	}
}