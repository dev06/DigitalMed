using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


	private Vector3 shootingPosition;

	private bool shot = false;

	private Transform defaultParent;

	private BulletEffects bulletEffects;

	void Start ()
	{
		defaultParent = transform.parent;

		bulletEffects = FindObjectOfType<BulletEffects>();

		Toggle(false);
	}

	// Update is called once per frame
	void Update ()
	{

		if (!shot) { return; }

		transform.forward = shootingPosition;

		transform.Translate(transform.forward * Time.deltaTime * 10f, Space.World);

	}

	public void ShootTowards(Vector3 position)
	{

		Toggle(true);

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

			bulletEffects.TriggerParticle(transform.position);

			transform.SetParent(defaultParent);

			transform.localPosition = Vector3.zero;

			Toggle(false);

			shot = false;
		}
	}

	public void Toggle(bool b)
	{
		foreach (Transform t in transform)
		{
			t.gameObject.SetActive(b);
		}
	}
}
