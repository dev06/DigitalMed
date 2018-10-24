using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {


	public bool CanDie = true;

	public static GameplayController Instance;

	public int checkpointsCollected;

	public bool collectedAllCheckpoints;

	private Transform _ghostContainer;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	}

	void OnEnable()
	{
		EventManager.OnCheckpointHit += OnCheckpointHit;
		EventManager.OnLevelComplete += OnLevelComplete;
	}

	void OnDisable()
	{
		EventManager.OnCheckpointHit -= OnCheckpointHit;
		EventManager.OnLevelComplete -= OnLevelComplete;
	}

	void Start()
	{
		_ghostContainer = GameObject.FindGameObjectWithTag("Containers/Ghost").transform;
	}

	void OnCheckpointHit()
	{
		checkpointsCollected++;

		collectedAllCheckpoints = (checkpointsCollected == LevelController.Instance.CurrentLevel.CheckpointCount);

		if (checkpointsCollected >= LevelController.Instance.CurrentLevel.CheckpointCount)
		{
			if (EventManager.OnStartHoverIdol != null)
			{
				EventManager.OnStartHoverIdol();
			}
		}
	}

	void OnLevelComplete()
	{
		checkpointsCollected = 0;
	}
}
