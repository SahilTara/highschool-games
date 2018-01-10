using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NPCDialog : MonoBehaviour {
    public AudioSource[] dialog;
    public int endingClips = 2;
	public int sceneIndex = 0;
	bool isEnding;
    int currentClip = 0;


	public bool IsEnding{
		get{ return isEnding; }
		set{ isEnding = value; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(currentClip > 0 && dialog.Length > 0 && !isEnding)
            if (dialog[currentClip-1].isPlaying) return;
        if (currentClip >= endingClips && !isEnding) return;
        if (currentClip > dialog.Length && isEnding)
        {
            SceneManager.LoadScene(sceneIndex);
        }
		if (dialog.Length > 0 && !isEnding) {
			dialog [currentClip].Play ();
		}
		currentClip++;
	}
}
