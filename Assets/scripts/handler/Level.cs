using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines a level in the level object container
public class Level : MonoBehaviour
{


	public int checkpointsInLevel; // number of checkpoints in the current level;

	//Containers
	public Transform CheckpointContainer;

	private Transform AIContainer;





	private Transform hover_ghost;

	private Transform ghostContainer;

	public void Init()
	{
		CheckpointContainer = transform.GetChild(1).transform;

		checkpointsInLevel = CheckpointContainer.childCount;
	}

	public void UpdateLevel()
	{
		AIContainer = transform.GetChild(2).transform;

		if (AIContainer.childCount <= 0) { return; }

		hover_ghost = AIContainer.GetChild(0);

		ghostContainer = GameObject.FindGameObjectWithTag("Containers/Ghost").transform;

		hover_ghost.SetParent(ghostContainer);
	}

	public int CheckpointCount
	{
		get
		{
			return checkpointsInLevel;
		}
	}
}
