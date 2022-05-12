using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : LifeForm {

	float threshold;
	List<int> objectsHit;
	Color color;
	GameObject explosionObject;
	//ParticleSystem explosion;
	//Rigidbody2D particleRb;

	ParticleSystem asd;


	private void Awake() {
		
		speed = 9f;
		threshold = 5.4f;
		life = 1;
		objectsHit = new List<int>();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		explosionObject = (GameObject)Resources.Load("Explosion");
		
	}

	// Use this for initialization
	void Start () {
	}

	public override void move() {
		transform.position = new Vector2 (transform.position.x + direction.x * speed * Time.deltaTime, transform.position.y + direction.y * speed * Time.deltaTime);
	}

	public override void checkPosition() {
		if (transform.position.y > screenLimitY * 1.5f || transform.position.y < -screenLimitY * 1.5f || transform.position.x > screenLimitX * 1.5f || transform.position.x < -screenLimitX * 1.5f) {
			Destroy(gameObject);
		}
	}

	public override void checkDeath() {
		if (life <= 0) {
			Destroy(gameObject);
		}
	}

	public void setDirection(Vector2 vector) {
		vector = normalize(vector);
		direction = vector;
	}

	public void setSpeed(float speed) {
		this.speed = speed;
	}

	public void setLife(int life) {
		this.life = life;
	}

	public void setColor(Color color) {
		this.color = color;
		spriteRenderer.color = color;
	}




	bool onHitList(int id) {
		return objectsHit.Contains(id);
	}

	void addOnHitList(int id) {
		objectsHit.Add(id);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if(this.tag == "PlayerShot" && !onHitList(collision.gameObject.GetInstanceID())){
			if (collision.gameObject.tag == "Enemy") {
				//addOnHitList(collision.gameObject.GetInstanceID());
				life--;
				collision.gameObject.GetComponent<LifeForm>().damage(1);

				GameObject explosion;

				//explosion.transform.SetPositionAndRotation(transform.position, new Quaternion(direction.x, direction.y, 0, 1));
				//explosionObject.transform.position = transform.position;
				if (life == 0) explosion = Instantiate(explosionObject, transform.position, Quaternion.identity);
				else explosion = Instantiate(explosionObject, transform.position, new Quaternion(-direction.y, direction.x, 0, 1)); // directions to make the cone effect work

				Destroy(explosion, 1f);

			}
		}
		else if(this.tag == "EnemyShot") {
			if(collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<PlayerBehaviour>().getImmune()) {
				//life--;
				collision.gameObject.GetComponent<LifeForm>().damage(1);
				//collision.gameObject.GetComponent<PlayerBehaviour>().makeImmune();
				print(collision.gameObject.tag);
				print(collision.gameObject.GetInstanceID());

			}
		}

		

	}

	// Update is called once per frame
	void Update () {
		move();
		checkPosition();
		checkDeath();
	}
}
