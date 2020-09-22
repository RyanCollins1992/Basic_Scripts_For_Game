using UnityEngine;
using System.Collections;

public class RandomSprite : MonoBehaviour {
	int  cactus1, cactus2;
	public GameObject hide1, hide2;
	// Use this for initialization
	void Start () {
		cactus1 = Random.Range (1,5);
		cactus2 = Random.Range (1,5);
		do{
			cactus2 = Random.Range (1,5);
		}while(cactus1 == cactus2);
		hide1 = GameObject.Find (cactus1.ToString());
		hide2 = GameObject.Find (cactus2.ToString());
		hide1.SetActive (false);
		hide2.SetActive (false);

	}

	// Update is called once per frame
	void Update () {

	}
}