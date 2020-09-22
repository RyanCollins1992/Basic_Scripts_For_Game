using UnityEngine;
using System.Collections;

public class CollectGem : MonoBehaviour {


	public AudioClip gem;
	void Start()
	{
		GetComponent<AudioSource> ().playOnAwake = false;
		GetComponent<AudioSource> ().clip = gem;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Player") {
			GetComponent<AudioSource> ().Play ();
			//ScoreHandler.addPoints (pointsToAdd);	
			//Destroy (gameObject);
		}

	}
	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.tag == "Player") {
			//GetComponent<AudioSource> ().Play ();
			//ScoreHandler.addPoints (pointsToAdd);	
			Destroy (gameObject);
		}

	}
}
