using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inutiu : LifeForm {

	protected Vector2 target;
	int phase;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void move() { }
	public override void checkPosition() { }
	public override void checkDeath() { }

	public override void shoot() { }

	public override void damage(int damage) {
		life -= damage;
	}



	public override GameObject createShot(Vector2 direction, float speed = 0.15f, int life = 1, Color? color = null, float angleIncForSpreadShot = 20f) {
		GameObject shot = base.createShot(direction, speed, life, color, angleIncForSpreadShot);
		return shot;
	}
}
