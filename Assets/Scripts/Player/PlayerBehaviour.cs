using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : LifeForm {



	enum key        {up,   down,  left,  right, shoot, special, dash};
	bool[] keyMap = {false, false, false, false, false, false, false};

	float dashedCounter;
	float dashedTime;

	float speedSlow;
	float speedFast;
	float maxSpeed;
	float acceleration;
	float deceleration;
	float baseDeceleration;
	protected Vector2 velocity;

	bool hasChild;
	bool asteroidControl;

	float shotCooldown;
	float shotCdCounter;
	float time;
	float immunityTime;
	float immunityCounter;
	float turnAngle;

	GameObject childX;
	GameObject childY;
	GameObject childXY;

	bool immune;


	// Use this for initialization
	void Start () {

		asteroidControl = true;

		immune = false;
		immunityCounter = 0f;
		immunityTime = 1f;
		shotCooldown = 0.3f;
		//normalShotCooldown = 0.03f;
		shotCdCounter = 0f;
		shotQuantity = 2;

		turnAngle = 140f * Time.deltaTime;
		velocity = new Vector2(0,0);
		direction = new Vector2(0,1);

		speedFast = 15f;
		speedSlow = 10f;
		speed = 0;

		baseDeceleration = 2f;
		deceleration = baseDeceleration;
		acceleration = 2f + deceleration;
		dashedCounter = 0;
		dashedTime = 1f;

		//setShotPattern(new SpreadShot());

		shotSpeed = 9f;
		pierceChance = 100;
		pierceMiss = 0;
		time = GameManager.inc;
		life = 200;
		shot = (GameObject)Resources.Load("Shot");
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();



		childX = Instantiate((GameObject)Resources.Load("PlayerClone"),  new Vector3(500, 0, 0), transform.rotation);
		childY = Instantiate((GameObject)Resources.Load("PlayerClone"),  new Vector3(500, 0, 0), transform.rotation);
		childXY = Instantiate((GameObject)Resources.Load("PlayerClone"), new Vector3(500, 0, 0), transform.rotation);

		childX.transform.localScale = transform.localScale;
		childY.transform.localScale = transform.localScale;
		childXY.transform.localScale = transform.localScale;

		childX.GetComponent<PlayerClone>().setFather(this);
		childY.GetComponent<PlayerClone>().setFather(this);
		childXY.GetComponent<PlayerClone>().setFather(this);

		

	}

	void input() {
		if (Input.GetKeyDown(KeyCode.W)) {
			keyMap[(int)key.up] = true;
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			keyMap[(int)key.down] = true;
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			keyMap[(int)key.left] = true;
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			keyMap[(int)key.right] = true;
		}
		if (Input.GetKeyDown(KeyCode.U)) {
			keyMap[(int)key.shoot] = true;
		}
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			keyMap[(int)key.dash] = true;
		}



		if (Input.GetKeyUp(KeyCode.W)) {
			keyMap[(int)key.up] = false;
		}
		if (Input.GetKeyUp(KeyCode.S)) {
			keyMap[(int)key.down] = false;
		}
		if (Input.GetKeyUp(KeyCode.A)) {
			keyMap[(int)key.left] = false;
		}
		if (Input.GetKeyUp(KeyCode.D)) {
			keyMap[(int)key.right] = false;
		}
		if (Input.GetKeyUp(KeyCode.U)) {
			keyMap[(int)key.shoot] = false;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			keyMap[(int)key.dash] = false;
		}
	}

	void controlSpeed() {
		dashedCounter -= time * Time.deltaTime;
		float velocitySize = vectorSize(velocity);
		Vector2 normalizedVelocity = normalize(velocity);

		//if(velocitySize > 0) velocity -= new Vector2 (deceleration * Time.deltaTime * normalizedVelocity.x, deceleration * Time.deltaTime * normalizedVelocity.y);
		if(velocitySize > 0) velocity *= 0.99f;
		if (velocitySize > 0 && velocitySize < deceleration * Time.deltaTime) velocity = new Vector2(0,0);



		maxSpeed = speedSlow;
		if (keyMap[(int)key.dash]) {
			maxSpeed = speedFast;
			//dashedCounter = dashedTime;
		}

		deceleration = baseDeceleration;
		if(maxSpeed == speedSlow) {
			if(velocitySize > maxSpeed || velocitySize <= -maxSpeed * 0.5f) {
				if(dashedCounter > 0) {
					deceleration = acceleration + baseDeceleration * 0.5f;
				}
				//else if(velocitySize >= maxSpeed) velocity = normalize(velocity + new Vector2(normalizedVelocity.x * maxSpeed, normalizedVelocity.y * maxSpeed)) * maxSpeed;
			}
			//else dashedCounter = 0f;
		}
		if (velocitySize >= maxSpeed) velocity = normalize( velocity + new Vector2(normalizedVelocity.x * maxSpeed , normalizedVelocity.y * maxSpeed)) * maxSpeed;


	}

	public override void move() {
		Vector2 position = transform.position;

		if (asteroidControl) {

			if (keyMap[(int)key.up]) {
				velocity += new Vector2(direction.x * acceleration * Time.deltaTime, direction.y * acceleration * Time.deltaTime);

			}

			if (keyMap[(int)key.down]) {
				velocity -= new Vector2(direction.x * acceleration * Time.deltaTime, direction.y * acceleration * Time.deltaTime);
			}

			if (keyMap[(int)key.left]) {
				direction = rotateVector(direction, turnAngle * Mathf.Deg2Rad);
				transform.Rotate(Vector3.forward * turnAngle);
			}

			if (keyMap[(int)key.right]) {
				direction = rotateVector(direction, -turnAngle * Mathf.Deg2Rad);
				transform.Rotate(Vector3.forward * -turnAngle);
			}

			controlSpeed();

			//transform.position = new Vector2(transform.position.x + speed * direction.x * Time.deltaTime, transform.position.y + speed * direction.y * Time.deltaTime);


			transform.position = new Vector2(transform.position.x + velocity.x * Time.deltaTime, transform.position.y + velocity.y * Time.deltaTime);
		} 
		else {; } //

	}

	public override void checkDeath() {
		if (life <= 0) {
			Destroy(gameObject);
		}
	}

	public override void checkPosition() {

		if (asteroidControl) {
			if (transform.position.x <= -screenLimitX) {
				transform.position = new Vector2(screenLimitX, transform.position.y);
			}
			else if (transform.position.x >= screenLimitX) {
				transform.position = new Vector2(-screenLimitX, transform.position.y);
			}

			if (transform.position.y <= -screenLimitY) {
				transform.position = new Vector2(transform.position.x, screenLimitY);
			}
			else if (transform.position.y >= screenLimitY) {
				transform.position = new Vector2(transform.position.x, -screenLimitY);
			}



			if (transform.position.x <= -screenLimitX + size * transform.localScale.x) {
				childX.transform.position = new Vector3(transform.position.x + screenLimitX * 2, transform.position.y, 0);
				childX.transform.rotation = transform.rotation;
				childXY.transform.position = new Vector3(transform.position.x + screenLimitX * 2, childXY.transform.position.y, 0);
				childXY.transform.rotation = transform.rotation;
				;
			}
			else if (transform.position.x >= screenLimitX - size * transform.localScale.x) {
				childX.transform.position = new Vector3(transform.position.x - screenLimitX * 2, transform.position.y, 0);
				childX.transform.rotation = transform.rotation;
				childXY.transform.position = new Vector3(transform.position.x - screenLimitX * 2, childXY.transform.position.y, 0);
				childXY.transform.rotation = transform.rotation;
				;
			}
			else {
				childX.transform.position = new Vector3(500, 0, 0);
				childXY.transform.position = new Vector3(500, 0, 0);
				
			}
			if (transform.position.y <= -screenLimitY + size * transform.localScale.y) {
				childY.transform.position = new Vector3(transform.position.x , transform.position.y + screenLimitY * 2, 0);
				childY.transform.rotation = transform.rotation;
				childXY.transform.position = new Vector3(childXY.transform.position.x, transform.position.y + screenLimitY * 2, 0);
				childXY.transform.rotation = transform.rotation;
				;
			}
			else if (transform.position.y >= screenLimitY - size * transform.localScale.y) {
				childY.transform.position = new Vector3(transform.position.x , transform.position.y - screenLimitY * 2, 0);
				childY.transform.rotation = transform.rotation;
				childXY.transform.position = new Vector3(childXY.transform.position.x, transform.position.y - screenLimitY * 2, 0);
				childXY.transform.rotation = transform.rotation;
				;
			}
			else {
				childY.transform.position = new Vector3(500, 0, 0);
				childXY.transform.position = new Vector3(500, 0, 0);
			}


		}
		else {
			if (transform.position.x <= -screenLimitX) {
				transform.position = new Vector2(-screenLimitX, transform.position.y);
				Debug.Log("asdasdasd");
			}
			else if (transform.position.x >= screenLimitX) {
				transform.position = new Vector2(screenLimitX, transform.position.y);
				Debug.Log("asdasdasd");
			}

			if (transform.position.y <= -screenLimitY) {
				transform.position = new Vector2(transform.position.x, -screenLimitY);
			}
			else if (transform.position.y >= screenLimitY) {
				transform.position = new Vector2(transform.position.x, screenLimitY);
			}
		}

	}

	public override void damage(int damage) {
		base.damage(damage);
		makeImmune();
	}

	public override GameObject createShot(Vector2 direction, float speed = 0.15f, int life = 1, Color? color = null, float angleIncForSpreadShot = 20f) {
		GameObject shot = base.createShot(direction, speed, life, color, angleIncForSpreadShot);
		//if (shot != null) shot.tag = "PlayerShot";
		return shot;
	}

	public override void shoot() {
		if (keyMap[(int)key.shoot] && shotCdCounter <= 0) {
			shotCdCounter = shotCooldown;
			GameObject shot;


			if (Random.Range(0,100) < pierceChance + pierceMiss) {
				shot = createShot(direction, shotSpeed, 2);
				pierceMiss = 0;
			}
			else{
				shot = createShot(direction, shotSpeed);
				pierceMiss++;
			}
		}
		shotCdCounter -= time * Time.deltaTime;
	}
	
	void checkImmunity() {
		if (immune) {
			immunityCounter -= time * Time.deltaTime;
			if (immunityCounter <= 0) {
				immune = false;
				spriteRenderer.color = Color.white;
			}
		}
	}

	public void makeImmune() {
		immune = true;
		immunityCounter = immunityTime;
		spriteRenderer.color = Color.blue;
	}

	public virtual bool getImmune() {
		return immune;
	}

	public Transform getTransform() {
		return transform;
	}

	public PolygonCollider2D getCollider() {
		return gameObject.GetComponent<PolygonCollider2D>();
	}

	void debugLines() {
		Debug.DrawLine(transform.position, new Vector3(transform.position.x + direction.x * 4, transform.position.y + direction.y * 4, transform.position.z), Color.yellow);
	}

	// Update is called once per frame
	void Update () {
		input();
		move();
		//spawnChild();
		checkPosition();
		shoot();
		checkDeath();
		checkImmunity();
		debugLines();
	}
	
	
}
