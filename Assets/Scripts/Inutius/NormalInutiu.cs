using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalInutiu : Inutiu {
	float normalShotCooldown;
	float boladoShotCooldown;
	float time;
	float shotCdCounter;
	
	bool middleShot;
	bool chargingMiddleShot;
	bool shotThisFrame;

	float shotAngle;
	int numberOfMiddleShots;
	int middleShotsCounter;

	Color normalColor;
	Color boladoColor;
	
	Animator animator;


	private void Awake() {
		middleShot = false;
		chargingMiddleShot = false;
		middleShotsCounter = 0;
		numberOfMiddleShots = 20;
		speed = 3f;
		life = 10;
		life = 100;
		time = GameManager.inc;
		normalShotCooldown = 0.2f;
		boladoShotCooldown = 0.1f;
		
		
		shotQuantity = 3;
		baseShotQuantity = 2;
		

		direction.x = 1;
		direction.y = 0;
		
		shotCdCounter = 0f;
		shot = (GameObject)Resources.Load("Shot");
		shotSpeed = 18f;
		shotAngle = 40;

		animator = GetComponent<Animator>();
		
		animator.Play("normalInutiuIdle");


		normalColor = Color.green;
		boladoColor = Color.red;

		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = normalColor;
		shotColor = spriteRenderer.color - new Color(0.3f, 0.3f, 0, 0);
	}


	// Use this for initialization
	void Start () {

	}

	public override void move() {
		if (!chargingMiddleShot)
			transform.position = new Vector2(transform.position.x + speed * direction.x * Time.deltaTime, transform.position.y + speed * direction.y * Time.deltaTime);
	}

	public override void damage(int damage) {
		base.damage(damage);
	}



	public override void checkDeath() {
		if(life <= 0) {
			Destroy(gameObject);
		}
	}


	public override GameObject createShot(Vector2 direction, float speed = 9f, int life = 1, Color? color = null, float angleIncForSpreadShot = 20f) {
		GameObject shot = base.createShot(direction, speed, life, color, angleIncForSpreadShot);
		//shot.tag = "EnemyShot";
		return shot;
	}



	public override void shoot() {
		if (shotCdCounter <= 0) {
			Vector2 shotDirection;
			GameObject player = GameObject.FindGameObjectWithTag("Player");



			if (player != null){
				target = player.GetComponent<Transform>().position;
				shotDirection = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
			}
			else shotDirection = new Vector2(0, -1);

			shotQuantity = baseShotQuantity;
			shotAngle = 40;
			if(middleShot) {
				shotQuantity += 1;
				shotAngle = 20;
			}

			if(chargingMiddleShot){
				shotQuantity = 0;
			}

			createShot(shotDirection, shotSpeed, 1, shotColor, shotAngle);
			shotThisFrame = true;
			shotCdCounter = normalShotCooldown;
		}
		shotCdCounter -= time * Time.deltaTime;
	}

	void updateMiddleShots() {
		if(shotThisFrame){
			if (middleShot) {
				middleShotsCounter--;
				if(middleShotsCounter == 0) {
					middleShot = false;
					spriteRenderer.color = normalColor;
					animator.Play("normalInutiuIdle");
				}
			}
			else {
				middleShotsCounter++;
				if(middleShotsCounter >= numberOfMiddleShots - 5) {
					chargingMiddleShot = true;
					//spriteRenderer.color = boladoColor;
				}
				if(middleShotsCounter == numberOfMiddleShots){
					chargingMiddleShot = false;
					middleShot = true;
					shotCdCounter = 0;

					
					animator.Play("normalInutiuShooting");
				}
			}
		}
	}

	void changeColor() {
		float colorIncrement = 0.1f;

		if (chargingMiddleShot) {
			spriteRenderer.color = boladoColor;
		}
		else if (middleShot && shotThisFrame) {
			Color auxColor = spriteRenderer.color;
			if(middleShotsCounter >= numberOfMiddleShots >> 1) {
				auxColor.g += colorIncrement;
			}
			else {
				auxColor.r -= colorIncrement;
			}
			spriteRenderer.color = auxColor;
			shotColor = spriteRenderer.color;
			shotColor -= new Color(0.3f, 0.3f, 0, 0);
		}


	}

	void OnParticleCollision(GameObject other) {
		Debug.Log(other);
	}


	// Update is called once per frame
	void Update () {
		shotThisFrame = false;
		checkDeath();
		move();
		changeDirection();
		shoot();
		updateMiddleShots();
		changeColor();
	}

	private void FixedUpdate() {
		//changeColor();		
	}

}
