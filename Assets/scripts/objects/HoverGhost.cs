using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverGhost : MonoBehaviour {



	private SpawnerHandler spawnerHandler;

	private float speed = 10;

	private Vector3 targetPosition;

	private Vector3 lastPosition;

	private Transform bulletContainer;

	public GameObject bulletPrefab;

	private PlayerMovement player;

	private float shootingTimer;

	private float shootingCooldown = .5f;

	void Start ()
	{
		spawnerHandler = FindObjectOfType<SpawnerHandler>();

		targetPosition = transform.position;

		bulletContainer = transform.GetChild(0).transform;

		player = FindObjectOfType<PlayerMovement>();

		CreateBullets();

		InvokeRepeating("GetNewPosition", 0, 5f);
	}

	void CreateBullets()
	{
		for (int i = 0; i < 5; i++)
		{
			GameObject clone = Instantiate(bulletPrefab) as GameObject;

			clone.transform.SetParent(bulletContainer);

			clone.transform.localPosition = Vector3.zero;
		}
	}

	void Shoot()
	{
		if (bulletContainer.transform.childCount <= 0) { return; }

		bulletContainer.transform.GetChild(0).GetComponent<Bullet>().ShootTowards(player.transform.position);
	}


	void Update ()
	{
		if (Vector3.Distance(transform.position, player.transform.position) > 20) { return; }

		UpdateShooting();

		Vector3 bump = new Vector3(0, Mathf.PingPong(Time.time, 1f), 0);

		transform.position = Vector3.MoveTowards(transform.position, targetPosition + bump  , Time.deltaTime * speed);
	}

	private void UpdateShooting()
	{
		shootingTimer += Time.deltaTime;

		if (shootingTimer > shootingCooldown)
		{
			Shoot();
			shootingTimer = 0;
		}
	}


	void LateUpdate()
	{
		Vector3 direction = transform.position - lastPosition;

		if (direction != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

			Quaternion rot = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

			rot.x = 0;

			rot.z = 0;

			transform.rotation = rot;
		}
		lastPosition = transform.position;
	}

	private void GetNewPosition()
	{
		targetPosition = new Vector3(0, 10, 0) +  spawnerHandler.GetPositionOnMap(15, 10);
	}
}
