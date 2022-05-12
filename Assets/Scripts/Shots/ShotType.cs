using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotType : MonoBehaviour {

	protected GameObject shotPrefab;
	protected float shotMaxAngle;

	// Use this for initialization
	void Start () {
		shotPrefab = (GameObject)Resources.Load("Shot");
		shotMaxAngle = 10;
	}
	
	public virtual void instantiateShots(LifeForm shooter, int shotQuantity, Vector2 position, int life = 1, float inc = 0.2f) {}


	protected Vector2 normalize(Vector2 vector) {
		float size = Mathf.Sqrt(vector.x * vector.x  +  vector.y * vector.y);
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

}
