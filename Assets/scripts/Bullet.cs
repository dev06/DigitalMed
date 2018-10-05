using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Vector3 shootingPosition;
	private bool shot = false;
	private Transform defaultParent;
	void Start ()
	{
		defaultParent = transform.parent;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!shot) { return; }
		transform.forward = shootingPosition;
		transform.Translate(transform.forward * Time.deltaTime * 10f, Space.World);
		//transform.position = Vector3.MoveTowards(transform.position, shootingPosition, Time.deltaTime * 5f);
	}

	public void ShootTowards(Vector3 position)
	{
		shot = true;
		shootingPosition =  position - transform.position;
		transform.parent = null;
	}

	void OnCollisionEnter(Collision col)
	{

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Platform")
		{
			transform.SetParent(defaultParent);
			transform.localPosition = Vector3.zero;
			shot = false;
		}
	}
}
