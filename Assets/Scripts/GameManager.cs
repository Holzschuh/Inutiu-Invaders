using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	enum bossNum { normalInutiu, evadingInutiu };
	string [] bosses = { "normalInutiu", "evadingInutiu" };

	public static float inc = 0.6f;

	public int boss;

	bool playerAlive;
	bool playerSpawned;
	bool inutiuSpawned;    // make Wave an object with number and type of Inutius it has

	bool test;

	float waveWaitTime;
	float waveWaitTimeCounter;

	float screenLimitX;
	float screenLimitY;

	public static float trueScreenLimitX;
	public static float trueScreenLimitY;

	GameObject playerClass;
	GameObject inutiuClass;
	GameObject player;
	GameObject inutiu;

	public GameObject playerClassPrefab;
	public Transform playerSpawnTransform;

	SpreadShot spread;
	NormalShot normal;
	NormalInutiuShot normalInutiuShot;

	public Vector2 WorldUnitsInCamera;
	public Vector2 WorldToPixelAmount;

	public Camera camera;

	// Use this for initialization
	void Start()
	{
		Application.targetFrameRate = 60;

		camera = Camera.main;

		float halfHeight = camera.orthographicSize;
		float halfWidth = camera.aspect * halfHeight;

		screenLimitY = halfHeight;
		screenLimitX = halfWidth;


		test = false;

		playerAlive = true;
		boss = (int) bossNum.normalInutiu;

		waveWaitTime = 3f;
		waveWaitTimeCounter = 3f;

		spread = gameObject.AddComponent<SpreadShot>();
		normal = gameObject.AddComponent<NormalShot>();
		normalInutiuShot = gameObject.AddComponent<NormalInutiuShot>();

		inutiuClass = (GameObject) Resources.Load(bosses [boss]);

		inutiu = Instantiate(inutiuClass, inutiuClass.transform.position, Quaternion.identity);
		player = Instantiate(playerClassPrefab, playerSpawnTransform.position, Quaternion.identity);

		inutiu.GetComponent<Inutiu>().setScreenLimits(screenLimitX, screenLimitY);
		inutiu.GetComponent<LifeForm>().setTrueScreenLimits(trueScreenLimitX, trueScreenLimitY);
		player.GetComponent<PlayerBehaviour>().setScreenLimits(screenLimitX, screenLimitY);

		setShotPatternOfObjectTo(inutiu, normalInutiuShot);
		setShotPatternOfObjectTo(player, spread);


		player.GetComponent<PlayerBehaviour>().setSize(player.GetComponent<SpriteRenderer>().size.x * 2);

	}

	void increments()
	{
		waveWaitTimeCounter -= inc * Time.deltaTime;
	}

	void Finalchecks()
	{
		if (waveWaitTimeCounter <= 0)
		{
			waveWaitTimeCounter = 0;
		}
	}

	float getScreenLimitX()
	{
		return screenLimitX;
	}
	float getScreenLimitY()
	{
		return screenLimitY;
	}

	void setShotPatternOfObjectTo(GameObject gameObject, ShotType shotType)
	{
		gameObject.GetComponent<LifeForm>().setShotPattern(shotType);
	}

	// Update is called once per frame
	void Update()
	{
		increments();
	}
}
