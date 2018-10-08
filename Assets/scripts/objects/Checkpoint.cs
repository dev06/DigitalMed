using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	PlayerMovement movementHandler;

	Vector3 targetPosition;

	private SpawnerHandler spawner;

	void Start ()
	{
		movementHandler = FindObjectOfType<PlayerMovement>();

		spawner = FindObjectOfType<SpawnerHandler>();

		transform.position = GetNextLocation();
	}

	void Update () {

		//transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Player") { return; }

		if (EventManager.OnCheckpointHit != null)
		{
			EventManager.OnCheckpointHit();
		}

		transform.position = GetNextLocation();



		//	GenerateCheckpointPosition();
	}

	private Vector3 GetNextLocation()
	{
		Vector3 location = Vector3.up;

		Transform currentLevelObjectLocations = LevelController.Instance.CurrentLevelObject.GetChild(1);

		location = currentLevelObjectLocations.GetChild(GameplayController.Instance.checkpointsCollected).position;

		return location;
	}

	// Generates new checkpoint in the world
	private void GenerateCheckpointPosition()
	{
		float distance = Vector3.Distance(transform.position, movementHandler.transform.position);

		do
		{
			float x = Random.Range(-7, 7);
			float z = Random.Range(-7, 7);

			Vector3 genPosition = new Vector3(x, 0, z);

			distance = Vector3.Distance(genPosition, movementHandler.transform.position);

			targetPosition = new Vector3(x, 0, z);

		} while (distance < 5);
	}
}
