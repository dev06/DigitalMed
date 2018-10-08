using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour {

	private List<Transform> levelObjects = new List<Transform>();

	void Start ()
	{
		for (int i = 0 ; i < transform.childCount; i++)
		{
			levelObjects.Add(transform.GetChild(i));
		}

		ToggleLevelObject(0);
	}

	void Update () {

	}

	public void ToggleLevelObject(int index)
	{
		if (index > levelObjects.Count - 1) return;

		for (int i = 0; i < levelObjects.Count; i++)
		{
			levelObjects[i].gameObject.SetActive(i == index);
		}

		LevelController.Instance.CurrentLevelObject = levelObjects[index];
	}
}
