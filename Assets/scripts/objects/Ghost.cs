
// Ghost class. Resembles each ghost in the world

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

	public List<Vector3> path = new List<Vector3>();

	private Vector3 lerpPosition;

	private Vector3 lastPosition;

	private TrailRenderer trail;

	//Initalizes the ghost with the path
	public void Init(List<Vector3> path)
	{
		trail = GetComponentInChildren<TrailRenderer>();
		trail.endWidth = 0f;
		SetPath(path);
		TraversePath();
		trail.Clear();
		transform.position = path[0];
		lerpPosition = transform.position;
	}

	public void SetPath(List<Vector3> path)
	{
		this.path = path;
	}

	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, lerpPosition, Time.deltaTime * 10f);
	}

	void LateUpdate()
	{
		Vector3 direction = transform.position - lastPosition;
		if (direction != Vector3.zero)
		{

			Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
			Quaternion rot = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50f);
			rot.x = 0;
			rot.z = 0;
			transform.rotation = rot;
		}
		lastPosition = transform.position;

	}

	public void TraversePath()
	{
		if (path == null || (path != null && path.Count == 0))
		{
			return;
		}


		if (gameObject.activeSelf)
		{
			StopCoroutine("ITravesePath");
			StartCoroutine("ITravesePath");
		}

	}

	IEnumerator ITravesePath()
	{
		int index = 0;

		int direction = 1;

		Vector3 heightOffset = new Vector3(0, 2, 0);

		while (true)
		{
			lerpPosition = path[index] + heightOffset;

			index += direction;

			if (index >= path.Count - 1 || index <= 0)
			{
				direction *= -1;
			}
			yield return new WaitForSeconds(.01f);
		}

		StopCoroutine("ITravesePath");
	}

	public void Toggle(bool b)
	{
		transform.gameObject.SetActive(b);
	}
}
