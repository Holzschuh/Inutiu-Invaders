using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStuff : MonoBehaviour {

	public static float screenLimitX;
	public static float screenLimitY;
	public static GlobalStuff self;

	// Use this for initialization
	void Awake () {
		self = this;
		screenLimitX = 5.24f;
		screenLimitY = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
