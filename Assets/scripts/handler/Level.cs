using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines a level in the level object container
public class Level : MonoBehaviour
{


	public int checkpointsInLevel; // number of checkpoints in the current level;

	//Containers
	public Transform CheckpointContainer;

	public Transform KeyContainer;

	private Transform AIContainer;


	private Transform hover_ghost;

	private Transform ghostContainer;


	private int currentKeyIndex;


	void OnEnable()
	{
		EventManager.OnKeyCollected += OnKeyCollected;
	}
	void OnDisable()
	{
		EventManager.OnKeyCollected -= OnKeyCollected;
	}

	public void Init()
	{
		CheckpointContainer = transform.GetChild(1).transform;

		checkpointsInLevel = CheckpointContainer.childCount;

		ToggleKeys();
	}

	public void UpdateLevel()
	{
		AIContainer = transform.GetChild(2).transform;

		Debug.Log(AIContainer.childCount);

		if (AIContainer.childCount <= 0) { return; }

		hover_ghost = AIContainer.GetChild(0);

		ghostContainer = GameObject.FindGameObjectWithTag("Containers/Ghost").transform;

		hover_ghost.SetParent(ghostContainer);
	}

	private void OnKeyCollected()
	{
		if (KeyContainer == null) { return; }

		currentKeyIndex++;

		currentKeyIndex = Mathf.Clamp(currentKeyIndex, 0, KeyContainer.childCount);

		ToggleKeys();
	}

	private void ToggleKeys()
	{
		if (KeyContainer == null) { return; }

		for (int i = 0; i < KeyContainer.childCount; i++)
		{
			KeyContainer.GetChild(i).transform.gameObject.SetActive(false);
		}

		if (currentKeyIndex > KeyContainer.childCount - 1) { return; }
		KeyContainer.GetChild(currentKeyIndex).transform.gameObject.SetActive(true);
	}

	public int CheckpointCount
	{
		get
		{
			return checkpointsInLevel;
		}
	}
}
