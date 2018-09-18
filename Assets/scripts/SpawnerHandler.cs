﻿
//Spawner for the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler : MonoBehaviour {


	public static SpawnerHandler Instance; 

	public GameObject playerPrefab; 

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
	}

	//Method called when player hits a new checkpoint. 
	void OnCheckpointHit()
	{
	}

	//Add Ghost to the world with list of Vector2 path that it will follow
	public void AddGhost(List<Vector2> path)
	{
		GameObject clone = Instantiate(playerPrefab) as GameObject; 

		clone.transform.position = Vector3.zero; 

		clone.transform.GetComponent<Ghost>().Init(path); 
	}
}
