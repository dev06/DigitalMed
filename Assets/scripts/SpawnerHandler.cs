
//Spawner for the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler : MonoBehaviour {


	public static SpawnerHandler Instance; 

	public GameObject playerPrefab, obstaclePrefab; 

	public List<MovementHandler> Ghosts = new List<MovementHandler>(); 

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this; 
		}
	}

	void OnEnable()
	{
		EventManager.OnCheckpointHit+=OnCheckpointHit; 
	}
	void OnDisable()
	{
		EventManager.OnCheckpointHit-=OnCheckpointHit; 
	}

	void Start()
	{
		SpawnObstacles();
	}

	//Method called when player hits a new checkpoint. 
	void OnCheckpointHit()
	{
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			SpawnObstacles();
		}
	}

	//Add Ghost to the world with list of Vector2 path that it will follow
	public void AddGhost(List<Vector3> path)
	{
		GameObject clone = Instantiate(playerPrefab) as GameObject; 

		clone.transform.position = Vector3.zero; 

		clone.transform.GetComponent<Ghost>().Init(path); 
	}

	private void SpawnObstacles()
	{
		GameObject clone = Instantiate(obstaclePrefab) as GameObject; 

		Vector3 position = new Vector3(GetRandomFloat(6), 1, GetRandomFloat(6));

		clone.transform.position = position; 
	}

	private float GetRandomFloat(float threshold)
	{
		return Random.Range(0, 2) == 0 ? Random.Range(-10f, -threshold) : Random.Range(threshold, 10f); 
	}
}
