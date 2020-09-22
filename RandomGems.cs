using UnityEngine;
using System.Collections;

public class RandomGems : MonoBehaviour {
	int  gem1, gem2;
	public GameObject hide1, hide2;
	// Use this for initialization
	void Start () {
		gem1 = Random.Range (11,15);
		gem2 = Random.Range (11,15);
		do{
			gem2 = Random.Range (11,15);
		}while(gem1 == gem2);
		hide1 = GameObject.Find (gem1.ToString());
		hide2 = GameObject.Find (gem2.ToString());
		hide1.SetActive (false);
		hide2.SetActive (false);

	}

	// Update is called once per frame
	void Update () {

	}
}