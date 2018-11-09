using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {


	private Vector3 defaultPosition;
	private float amplitude = .2f;
	private float amplitudeSpeed = .2f;

	void Start ()
	{
		defaultPosition = transform.position;
	}


	void Update ()
	{
		transform.position = defaultPosition + new Vector3(0, Mathf.PingPong(Time.time * amplitudeSpeed, amplitude) + amplitude * .5f, 0);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			transform.gameObject.SetActive(false);

			if (EventManager.OnKeyCollected != null)
			{
				EventManager.OnKeyCollected();
			}
		}
	}
}
