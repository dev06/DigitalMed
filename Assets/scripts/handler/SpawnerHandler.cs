
//Spawner for the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler : MonoBehaviour {


	public static SpawnerHandler Instance;

	public GameObject playerPrefab, obstaclePrefab;

	public List<MovementHandler> Ghosts = new List<MovementHandler>();

	private List<GameObject> Obstacles = new List<GameObject>();

	public bool EnableGhost = true;

	private Transform _ghostContainer;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	void Start()
	{
		_ghostContainer = GameObject.FindGameObjectWithTag("Containers/Ghost").transform;
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SpawnObstacles();
		}
	}

	//Add Ghost to the world with list of Vector2 path that it will follow
	public void AddGhost(List<Vector3> path)
	{
		if (!EnableGhost) { return; }

		//if (LevelController.CURRENT_LEVEL > LevelObjects.LEVELS - 1) { return; }

		GameObject clone = Instantiate(playerPrefab) as GameObject;

		clone.transform.position = Vector3.zero;

		clone.transform.SetParent(_ghostContainer);

		clone.transform.GetComponent<Ghost>().Init(path);
	}

	private void SpawnObstacles()
	{
		GameObject clone = Instantiate(obstaclePrefab) as GameObject;

		Vector3 position = GetPositionOnMap();

		Vector3 scale = new Vector3(Random.Range(1f, 2f), Random.Range(2f, 3f), Random.Range(1f, 2f));

		clone.transform.localScale = scale;

		if (Obstacles.Count == 0)
		{
			clone.transform.position = position;

			Obstacles.Add(clone);
		}
		else
		{
			int c = 0;
			while (!IsValidPos(position))
			{
				position = GetPositionOnMap();
				c++;
				if (c > 1000)
				{
					Debug.Log("break");
					break;
				}
			}

			clone.transform.position = position;

			Obstacles.Add(clone);

		}
	}

	private bool IsValidPos(Vector3 position)
	{
		for (int i = 0; i < Obstacles.Count; i++)
		{

			if (Vector3.Distance(Obstacles[i].transform.position, position) < 5)
			{

				Debug.Log("Position is not valid");

				return false;
			}
		}

		return true;
	}

	public Vector3 GetCheckpointPosition()
	{
		Vector3 position = Vector3.zero;
		int c = 0;
		while (!IsValidPos(position))
		{
			position = GetPositionOnMap();
			c++;
			if (c > 1000)
			{
				Debug.Log("break");
				break;
			}
		}

		return position;
	}

	public Vector3 GetPositionOnMap(float mapSize = 10, float offset = 5)
	{
		float x = Random.Range(-10f, 10f);
		float y = 1;
		float z = Random.Range(-10f, 10f);

		Vector3 position = new Vector3(x, y, z);

		float distanceToCenter = Vector3.Distance(position, Vector3.zero);

		int breaker = 0;

		while (distanceToCenter < offset)
		{
			x = Random.Range(-mapSize, mapSize);
			y = 1;
			z = Random.Range(-mapSize, mapSize);

			position = new Vector3(x, y, z);

			distanceToCenter = Vector3.Distance(position, Vector3.zero);

			breaker++;

			if (breaker > 1000)
			{
				Debug.Log("Breaked");
				break;
			}
		}


		return position;
	}

}
