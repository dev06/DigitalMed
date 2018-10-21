using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines a level in the level object container
public class Level : MonoBehaviour
{

	private Transform AI;

	private Transform hover_ghost;
	private Transform ghostContainer;
	void Start()
	{

	}

	public void UpdateLevel()
	{
		AI = transform.GetChild(2).transform;

		hover_ghost = AI.GetChild(0);

		ghostContainer = GameObject.FindGameObjectWithTag("Containers/Ghost").transform;

		hover_ghost.SetParent(ghostContainer);
	}

}
