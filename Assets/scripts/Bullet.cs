using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Vector3 shootingPosition; 
	private bool shot = false; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!shot) return; 
		transform.position = Vector3.MoveTowards(transform.position, shootingPosition, Time.deltaTime * 5f); 
	}

	public void ShootTowards(Vector3 position)
	{
		shot = true; 
		this.shootingPosition = position; 
		transform.parent = null; 
	}
}
