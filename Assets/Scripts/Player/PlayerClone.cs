using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : PlayerBehaviour {

	Vector3 offset;
	PolygonCollider2D cloneCollider;
	PlayerBehaviour father;

	// Use this for initialization
	void Awake () {
		

	}

	private void Start() {
		;
	}

	public void setFather(PlayerBehaviour player) {
		father = player;
	}

	public override void move() {
	}

	public override bool getImmune() {
		return father.getImmune();
	}

	public override void damage(int damage) {
		father.damage(damage);
	}

	// Update is called once per frame
	void Update () {
		//copyParent();
	}
}
