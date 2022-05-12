using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEvadingInutiu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Vector2 enemy = collision.GetComponent<Transform>().position;

		if (enemy.x > transform.position.x) {
			gameObject.GetComponentInParent<EvadingInutiu>().dodge(new Vector2(-1, 0));
		}
		else {
			gameObject.GetComponentInParent<EvadingInutiu>().dodge(new Vector2(1, 0));
		}

		print("he can dooooodge?");

	}
}
