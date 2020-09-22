using UnityEngine;
using System.Collections;

public class Collectables : MonoBehaviour {
	public int pointsToAdd;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			ScoreManager.addPoints (pointsToAdd);	
			Destroy (gameObject);
		}
	}
}
