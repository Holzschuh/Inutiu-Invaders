using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalInutiuShot : ShotType
{

	void Start()
	{
		shotPrefab = (GameObject)Resources.Load("Shot");
		shotMaxAngle = 20;
	}

	public override void instantiateShots(LifeForm shooter, int shotQuantity, Vector2 position, int life = 1, float angleIncForSpreadShot = 6f)
	{
		GameObject shot;
		ShotScript shotScript;

		float angleInc = angleIncForSpreadShot;
		float shotAngle = angleInc / 2f;

		if (shotQuantity % 2 != 0 && shotQuantity > 0){
			shot = Instantiate(shotPrefab, position, Quaternion.identity);
			shot.tag = shooter.tag + "Shot";
			shotScript = shot.GetComponent<ShotScript>();
			shotScript.setDirection(shooter.getShotDirection());
			shotScript.setSpeed(shooter.getShotSpeed());
			shotScript.setLife(life);
			shotScript.setColor(shooter.getShotColor());
			shotScript.setScreenLimits(shooter.getScreenLimitX(), shooter.getScreenLimitY());

			shotQuantity--;
			shotAngle = angleInc;
		}

		while (shotQuantity > 0){

			shot = Instantiate(shooter.getShot(), position, Quaternion.identity);
			shot.tag = shooter.tag + "Shot";
			shotScript = shot.GetComponent<ShotScript>();
			shotScript.setDirection(rotateVector(shooter.getShotDirection(), Mathf.Deg2Rad * shotAngle));
			shotScript.setSpeed(shooter.getShotSpeed());
			shotScript.setLife(life);
			shotScript.setColor(shooter.getShotColor());
			shotScript.setScreenLimits(shooter.getScreenLimitX(), shooter.getScreenLimitY());


			shot = Instantiate(shooter.getShot(), position, Quaternion.identity);
			shot.tag = shooter.tag + "Shot";
			shotScript = shot.GetComponent<ShotScript>();
			shotScript.setDirection(rotateVector(shooter.getShotDirection(), Mathf.Deg2Rad * -shotAngle));
			shotScript.setSpeed(shooter.getShotSpeed());
			shotScript.setLife(life);
			shotScript.setColor(shooter.getShotColor());
			shotScript.setScreenLimits(shooter.getScreenLimitX(), shooter.getScreenLimitY());


			shotAngle += angleInc;
			shotQuantity -= 2;
		}

	}
}
