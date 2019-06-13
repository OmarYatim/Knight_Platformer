using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinM : MonoBehaviour {

	new AudioSource audio;
	Color tmp;
	// Use this for initialization
	void Start () {
		audio=GetComponent<AudioSource>();
		tmp = GetComponent<SpriteRenderer>().color;
		tmp.a = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			audio.Play();
			col.gameObject.GetComponent<PlayerScript>().score += 50;
			GetComponent<SpriteRenderer>().color = tmp;
			Destroy(gameObject.GetComponent<Collider2D>());
		}
	}
}
