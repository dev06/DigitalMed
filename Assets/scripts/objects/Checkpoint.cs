using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	PlayerMovement movementHandler;

	Vector3 targetPosition;

	private SpawnerHandler spawner;

	public void Init ()
	{
		movementHandler = FindObjectOfType<PlayerMovement>();

		spawner = FindObjectOfType<SpawnerHandler>();

		transform.position = GetNextLocation();

		Toggle(true);
	}

	private void Toggle(bool b)
	{
		GetComponent<MeshRenderer>().enabled = b;
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

	}

	private Vector3 GetNextLocation()
	{
		Vector3 location = Vector3.up;

		Transform currentLevelObjectLocations = LevelController.Instance.CurrentLevelObject.GetChild(1);

		location = currentLevelObjectLocations.GetChild(GameplayController.Instance.checkpointsCollected).position;

		return location;
	}


}
