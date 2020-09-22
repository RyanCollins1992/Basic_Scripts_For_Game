using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {
	public Text scoreText;
	public static float score;
	
	// Update is called once per frame
	void Update () {
		if (score < 0) 
			score = 0;
		
		scoreText.text = "" + score;
	
	}
	public static void addPoints(int pointsToAdd){
		score += pointsToAdd;
	}
}
