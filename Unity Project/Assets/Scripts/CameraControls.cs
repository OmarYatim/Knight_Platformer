using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
	
	[SerializeField]
	GameObject player;
	private float xMin = -76.1f;
	private float xMax = -21.4f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
		gameObject.transform.position = new Vector3(x,player.transform.position.y + 2.2f, -10);
	}
}
