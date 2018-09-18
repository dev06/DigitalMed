
// Ghost class. Resembles each ghost in the world

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

	public List<Vector2> path = new List<Vector2>();

	private Vector2 lerpPosition;

	private Vector2 lastPosition;

	private TrailRenderer trail;

	//Initalizes the ghost with the path
	public void Init(List<Vector2> path)
	{
		trail = GetComponentInChildren<TrailRenderer>();
		trail.endWidth = 0f;
		SetPath(path);
		TraversePath();
		transform.position = path[0];
		trail.Clear();
		lerpPosition = transform.position;
	}

	public void SetPath(List<Vector2> path)
	{
		this.path = path;
	}

	void Update()
	{
		Vector2 direction = (Vector2)transform.position - (Vector2)lastPosition;
		transform.up = direction;
		lastPosition = transform.position;
		transform.position = Vector2.Lerp(transform.position, lerpPosition, Time.deltaTime * 10f);
	}

	//Tranverse the path
	public void TraversePath()
	{
		if (path == null || (path != null && path.Count == 0)) { return; }

		StopCoroutine("ITravesePath");
		StartCoroutine("ITravesePath");
	}

	IEnumerator ITravesePath()
	{
		int index = 0;

		int direction = 1;

		while (true)
		{
			lerpPosition = path[index];

			index += direction;

			if (index >= path.Count - 1 || index <= 0)
			{
				direction *= -1;
			}
			yield return new WaitForSeconds(.022f);
		}

		StopCoroutine("ITravesePath");
	}
}
