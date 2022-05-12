using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadingInutiu : Inutiu {

	float deceleration;
	float decelerationMultiplier;
	float burstSpeed;
	float burstCooldown;
	float burstcdCounter;

	bool isSteady;
	bool heCanDooodge;
	bool stop;


	Vector2 targetPosition;

	float shotCooldown;
	float shotCdCounter;
	float stopCooldown;
	float stopCdCounter;
	float time;

	int shotAngle;

	Color normalColor;

	Animator animator;
	

	private void Awake() {

		deceleration = 2f;
		decelerationMultiplier = 6f;
		isSteady = true;
		heCanDooodge = true;

		stopCooldown = 0.3f;
		stopCdCounter = 0;
		stop = true;

		burstSpeed = 20f;
		burstCooldown = 1f;
		burstcdCounter = 0;
		speed = 0;

		direction = new Vector2(0,1);

		
		speed = 0f;
		life = 10;
		life = 100;
		time = GameManager.inc;
		shotCooldown = 0.1f;

		shotCdCounter = 0f;
		shot = (GameObject)Resources.Load("Shot");
		shotSpeed = 0.3f;
		shotAngle = 0;

		//animator = GetComponent<Animator>();

		//animator.Play("normalInutiuIdle");


		normalColor = Color.magenta;
		//boladoColor = Color.red;

		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.color = normalColor;
		shotColor = spriteRenderer.color - new Color(0.3f, 0.3f, 0, 0);
	}


	// Use this for initialization
	void Start() {

	}

	public override void move() {
		transform.position = new Vector2(transform.position.x + speed * direction.x * Time.deltaTime, transform.position.y + speed * direction.y * Time.deltaTime);
	}

	public override void damage(int damage) {
		base.damage(damage);
	}


	public override void checkDeath() {
		if (life <= 0) {
			Destroy(gameObject);
		}
	}


	public override GameObject createShot(Vector2 direction, float speed = 9f, int life = 1, Color? color = null, float increment = 20f) {
		GameObject shot = base.createShot(direction, speed, life, color, increment);
		return shot;
	}

	public void dodge(Vector2 direction) {

		if(heCanDooodge){
			this.direction = direction;
			applyBurstSpeed();
		}
	}

	public override void shoot() {

	}

	void randomizeTargetPosition() {
		do{
			targetPosition.x = Random.Range(-screenLimitX, screenLimitX);
			targetPosition.y = Random.Range(0, screenLimitY);
		}while(distanceBetween(transform.position, targetPosition) < 1f);

		direction.x = targetPosition.x - transform.position.x;
		direction.y = targetPosition.y - transform.position.y;

		direction = normalize(direction);
	}

	void applyBurstSpeed() {
		speed = burstSpeed;
		isSteady = false;
		burstcdCounter = burstCooldown;
		heCanDooodge = false;
		stopCdCounter = stopCooldown;
	}

	void controlBurst() {
		if (burstcdCounter <= 0) {
			heCanDooodge = true;
		}
	}

	void controlSpeed() {

		if(stop){
			speed -= deceleration * decelerationMultiplier * Time.deltaTime;
		}
		else speed -= deceleration * Time.deltaTime;

		if(speed <= 0) {
			speed = 0;
			isSteady = true;
		}
	}

	void inc() {
		if (isSteady) burstcdCounter -= time * Time.deltaTime;
		else stopCdCounter -= time * Time.deltaTime;

		if(stopCdCounter <= 0) stop = true;
	}


	// Update is called once per frame
	void Update() {

		checkDeath();
		controlSpeed();
		controlBurst();
		move();
		shoot();
		changeDirection();
		inc();
	}
}

