using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour {

	private List<Level> levels = new List<Level>();

	public static int LEVELS = 0;

	public void Init ()
	{
		for (int i = 0 ; i < transform.childCount; i++)
		{
			Level l = transform.GetChild(i).GetComponent<Level>();
			l.Init();
			levels.Add(l);
		}

		LEVELS = transform.childCount;

		ToggleLevelObject(0);
	}

	IEnumerator IWait(int index)
	{
		yield return new WaitForEndOfFrame();

		if (index > levels.Count - 1) { yield return null; }

		levels[index].UpdateLevel();

		for (int i = 0; i < levels.Count; i++)
		{
			levels[i].transform.gameObject.SetActive(i == index);
		}

		LevelController.Instance.CurrentLevel = levels[index];
	}

	public void ToggleLevelObject(int index)
	{


		StopCoroutine("IWait");
		StartCoroutine("IWait", index);
	}



}
