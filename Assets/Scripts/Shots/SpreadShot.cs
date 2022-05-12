using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : ShotType {

	void Start() {
		//shotPrefab = (GameObject)Resources.Load("Shot");
		shotMaxAngle = 20;
	}

	public override void instantiateShots(LifeForm shooter, int shotQuantity, Vector2 position, int life = 1, float angleIncForSpreadShot = 6f) {
		GameObject shot;
		ShotScript shotScript;

		float angleInc = angleIncForSpreadShot;
		float shotAngle = angleInc / 2f;

		shot = Instantiate(shooter.getShot(), position, Quaternion.identity);
		shot.tag = shooter.tag + "Shot";
		shotScript = shot.GetComponent<ShotScript>();
		shotScript.setDirection(shooter.getShotDirection());
		shotScript.setSpeed(shooter.getShotSpeed());
		shotScript.setLife(life);
		shotScript.setColor(shooter.getShotColor());
		shotScript.setScreenLimits(shooter.getScreenLimitX(), shooter.getScreenLimitY());

		shotQuantity--;
		shotAngle = angleInc;
		

		while (shotQuantity > 0) {

			shot = Instantiate(shooter.getShot(), position, Quaternion.identity);
			shot.tag = shooter.tag + "Shot";
			shotScript = shot.GetComponent<ShotScript>();
			shotScript.setDirection(rotateVector(shooter.getShotDirection(), Mathf.Deg2Rad * shotAngle));
			shotScript.setSpeed(shooter.getShotSpeed());
			shotScript.setLife(life);
			shotScript.setColor(shooter.getShotColor());
			shotScript.setScreenLimits(shooter.getScreenLimitX(), shooter.getScreenLimitY());
			if (shotQuantity == 1) shotScript.transform.localScale = new Vector3(shotScript.transform.localScale.x * 0.5f, shotScript.transform.localScale.y * 0.5f, 1);

			shot = Instantiate(shooter.getShot(), position, Quaternion.identity);
			shot.tag = shooter.tag + "Shot";
			shotScript = shot.GetComponent<ShotScript>();
			shotScript.setDirection(rotateVector(shooter.getShotDirection(), Mathf.Deg2Rad *-shotAngle));
			shotScript.setSpeed(shooter.getShotSpeed());
			shotScript.setLife(life);
			shotScript.setColor(shooter.getShotColor());
			shotScript.setScreenLimits(shooter.getScreenLimitX(), shooter.getScreenLimitY());
			if (shotQuantity == 1) shotScript.transform.localScale = new Vector3(shotScript.transform.localScale.x * 0.5f, shotScript.transform.localScale.y * 0.5f, 1);



			shotAngle += angleInc;
			shotQuantity -= 2;
		}

	}
}
