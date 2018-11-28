using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idolJitter : MonoBehaviour {

	private Vector3 defaultPosition;
	// Use this for initialization
	void Start ()
	{
		defaultPosition = transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.Lerp(transform.position, defaultPosition + Jitter(), Time.deltaTime * 20f);
	}

	public Vector3 Jitter()
	{
		return Random.insideUnitSphere * .4f;
	}
}
