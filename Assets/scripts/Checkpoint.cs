using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	MovementHandler movementHandler;
	
	Vector3 targetPosition; 

	void Start () 
	{
		movementHandler = FindObjectOfType<MovementHandler>(); 
		
		targetPosition = transform.position; 
	}
	
	void Update () {
		
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f); 
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag != "Player") return; 

		if(EventManager.OnCheckpointHit != null)
		{
			EventManager.OnCheckpointHit(); 
		}

		GenerateCheckpointPosition(); 
	}

	private void GenerateCheckpointPosition()
	{
		float distance = Vector3.Distance(transform.position, movementHandler.transform.position); 

		do
		{
			float min = .5f - Random.Range(0f, .4f); 
			float max = .5f + Random.Range(0f, .4f); 
			float boundX = Random.Range(0, 2) == 0 ? min : max; 
			float boundY = Random.Range(0, 2) == 0 ? min : max; 
			float x = Camera.main.ViewportToWorldPoint(new Vector3(boundX, 0, 0)).x; 
			float y = Camera.main.ViewportToWorldPoint(new Vector3(0, boundY, 0)).y; 
			
			Vector3 genPosition = new Vector3(x, y, 0); 

			distance = Vector3.Distance(genPosition, movementHandler.transform.position); 

			targetPosition = new Vector3(x, y, 0); 

		}while(distance < 5); 
	}
}
