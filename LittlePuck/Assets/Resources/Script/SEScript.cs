using UnityEngine;
using System.Collections;

public class SEScript : MonoBehaviour {
	public AudioSource SE;
	
	// Use this for initialization
	public void SetSE (string s) {
		SE = gameObject.AddComponent<AudioSource> ();
		SE.clip = Resources.Load (s)as AudioClip;
		//SE.loop = true;
		SE.Play ();
	}

	void Update(){
		if (SE != null && !SE.isPlaying) {
			Destroy (SE);
			Destroy (this);
		}
	}
}
