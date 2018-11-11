using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {


	public Key key;

	private PlayerMovement player;

	private bool isOpen;

	void Start ()
	{
		player = FindObjectOfType<PlayerMovement>();
	}

	void Update ()
	{

	}

	void OnCollisionEnter(Collision col)
	{

		if (col.gameObject.tag == "Player")
		{
			if (player.ContainsKey(key))
			{
				Open();
			}
		}
	}


	private void Open()
	{
		if (isOpen) { return; }
		StopCoroutine("IOpenGate");
		StartCoroutine("IOpenGate");
		isOpen = true;
	}

	IEnumerator IOpenGate()
	{
		Vector3 currentRotation = transform.eulerAngles;
		Vector3 targetRotation = currentRotation + new Vector3(0, 0, -90);
		Vector3 c = currentRotation;
		SFXController.Instance.SFXOpenGate();
		while (true)
		{

			transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.eulerAngles, targetRotation, Time.deltaTime * 10f));
			yield return null;
		}
	}
}
