using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {

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
	}

	void OnDisable()
	{
		EventManager.OnCheckpointHit -= OnCheckpointHit;
	}

	void Start()
	{
		_ghostContainer = FindObjectOfType<Transform>();
	}

	void OnCheckpointHit()
	{
		checkpointsCollected++;

		collectedAllCheckpoints = (checkpointsCollected == LevelController.Instance.CurrentLevelObject.GetChild(1).childCount);

		if (checkpointsCollected >= LevelController.Instance.CurrentLevelObject.GetChild(1).childCount)
		{
			if (EventManager.OnLevelComplete != null)
			{
				EventManager.OnLevelComplete();
			}

			foreach (Transform t in _ghostContainer)
			{
				t.gameObject.SetActive(false);
			}

			checkpointsCollected = 0;
		}
	}
}
