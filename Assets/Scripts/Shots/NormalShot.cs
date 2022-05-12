using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShot : ShotType {

	void Start() {
		//shotPrefab = (GameObject)Resources.Load("Shot");
		shotMaxAngle = 20;
	}

	public override void instantiateShots(LifeForm shooter, int shotQuantity, Vector2 position, int life = 1, float angleIncForSpreadShot = 0.2f) {
		GameObject shot;
		ShotScript shotScript;

		float positionInc = 0.2f;
		float positionDisplacement = positionInc / 2f;


		if (shotQuantity % 2 != 0   &&   shotQuantity > 0) {
			shot = Instantiate(shooter.getShot(), position, Quaternion.identity);
			shot.tag = shooter.tag + "Shot";
			shotScript = shot.GetComponent<ShotScript>();
			shotScript.setDirection(shooter.getShotDirection());
			shotScript.setSpeed(shooter.getShotSpeed());
			shotScript.setLife(life);
			shotScript.setColor(shooter.getShotColor());
			shotScript.setScreenLimits(shooter.getScreenLimitX(), shooter.getScreenLimitY());


			shotQuantity--;
			positionDisplacement = positionInc;
		}


		Vector2 perp = rotateVector(shooter.getShotDirection(), 90 * Mathf.Deg2Rad);
		perp = normalize(perp);

		while (shotQuantity > 0){


			shot = Instantiate(shooter.getShot(), position + perp * positionDisplacement, Quaternion.identity);
			shot.tag = shooter.tag + "Shot";
			shotScript = shot.GetComponent<ShotScript>();
			shotScript.setDirection(shooter.getShotDirection());
			shotScript.setSpeed(shooter.getShotSpeed());
			shotScript.setLife(life);
			shotScript.setColor(shooter.getShotColor());
			shotScript.setScreenLimits(shooter.getScreenLimitX(), shooter.getScreenLimitY());


			shot = Instantiate(shooter.getShot(), position - perp * positionDisplacement, Quaternion.identity);
			shot.tag = shooter.tag + "Shot";
			shotScript = shot.GetComponent<ShotScript>();
			shotScript.setDirection(shooter.getShotDirection());
			shotScript.setSpeed(shooter.getShotSpeed());
			shotScript.setLife(life);
			shotScript.setColor(shooter.getShotColor());
			shotScript.setScreenLimits(shooter.getScreenLimitX(), shooter.getScreenLimitY());


			//shotAngle -= positionInc;
			positionDisplacement += positionInc;

			shotQuantity -= 2;
		}
	}
}
