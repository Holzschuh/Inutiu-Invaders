using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeForm : MonoBehaviour {

	protected enum shotType {normal, spread};

	protected int shotQuantity;
	protected int baseShotQuantity;

	protected int life;
	protected float speed;
	protected float shotSpeed;
	protected float size;
	protected Vector2 direction;
	protected Vector2 shotDirection;
	protected GameObject shot;
	protected SpriteRenderer spriteRenderer;
	protected Color shotColor;
	protected ShotType shotPattern;

	protected int pierceChance;
	protected int pierceMiss;

	protected float screenLimitX;
	protected float screenLimitY;

	protected static float trueScreenLimitX;
	protected static float trueScreenLimitY;

	// Use this for initialization
	void Start () {
		//shotPattern = new NormalShot();
	}

	// Update is called once per frame
	void Update () {
		
	}

	protected Vector2 normalize(Vector2 vector) {
		float size = vectorSize(vector);
		vector = new Vector2(vector.x/size, vector.y/size);
		return vector;
	}

	protected float distanceBetween(Vector2 p1, Vector2 p2) {
		float distance;
		distance = Mathf.Sqrt((p2.x - p1.x) * (p2.x - p1.x)  +  (p2.y - p1.y) * (p2.y - p1.y));
		return distance;
	}

	protected Vector2 rotateVector(Vector2 vector, float rad) {
		Vector2 rotatedVector;
		float cos = Mathf.Cos(rad);
		float sin = Mathf.Sin(rad);

		rotatedVector.x = vector.x * cos - vector.y * sin;
		rotatedVector.y = vector.x * sin + vector.y * cos;

		return rotatedVector;
	}
	protected float vectorSize(Vector2 vector) {
		return Mathf.Sqrt(vector.x * vector.x  +  vector.y * vector.y);
	}


	public virtual void move() {}
	public virtual void checkPosition() {}
	public virtual void checkDeath() {}
	public virtual void shoot(){}

	public virtual void damage(int damage){
		life -= damage;
	}

	public void setSize(float size) {
		this.size = size;
	}


	public void setScreenLimits(float x, float y) {
		screenLimitX = x;
		screenLimitY = y;
	}

	public void setTrueScreenLimits(float x, float y) {
		trueScreenLimitX = x;
		trueScreenLimitY = y;
	}

	public float getScreenLimitX() {
		return screenLimitX;
	}
	public float getScreenLimitY() {
		return screenLimitY;
	}

	public void setShotPattern(ShotType shotPattern) {
		this.shotPattern = shotPattern;
	}

	public void removeShotPattern() {
		Destroy(shotPattern);
	}

	protected void changeDirection() {
		if ((transform.position.x > screenLimitX  &&  direction.x > 0)    ||    (transform.position.x < -screenLimitX  &&  direction.x < 0))
			direction.x *= -1;
		if ((transform.position.y > screenLimitY  &&  direction.y > 0)    ||    (transform.position.y < -screenLimitY  &&  direction.y < 0))
			direction.y *= -1;
	}

	public virtual GameObject createShot(Vector2 direction, float speed = 9f, int life = 1, Color? color = null, float increment = 20f) {
		if (Random.Range(0, 10) >= 0) {
			if (color == null) color = Color.blue;
			shotDirection = direction;
			shotSpeed = speed;
			//pierceChance = 100;
			shotColor = (Color)color;

			shotPattern.instantiateShots(this, shotQuantity, transform.position, life, increment);
			
			return shot;
		}
		return null;

	}




	public Color getShotColor() {
		return this.shotColor;
	}

	public float getShotSpeed() {
		return shotSpeed;
	}

	public int getPierceChance() {
		return pierceChance;
	}

	public Vector2 getShotDirection() {
		return shotDirection;
	}

	public GameObject getShot() {
		return shot;
	}
}