using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour {

	private Vector3 currentPosition, lastPosition; 
	private float xPosition, yPosition; 
	private float senstivity = 250f;
	private bool startedRecording; 
	private List<Vector2> recordingPosition = new List<Vector2>(); 
	private bool hasPressedDown;
	private TrailRenderer trail; 

	void OnEnable()
	{
		EventManager.OnCheckpointHit+=OnCheckpointHit; 
	}
	void OnDisable()
	{
		EventManager.OnCheckpointHit-=OnCheckpointHit; 
	}

	void Start()
	{
		trail = GetComponentInChildren<TrailRenderer>(); 
		trail.endWidth = 0; 
		pointerDown = Vector2.zero; 
		currentPosition = Vector2.zero; 
		xVel = 0; 
		yVel = 0; 

	}

	void OnCheckpointHit()
	{
		StopCoroutine("IRecordPosition"); 
		
		List<Vector2> temp = new List<Vector2>();
		
		for(int i = 0; i < recordingPosition.Count; i++)
		{
			temp.Add(recordingPosition[i]);
		} 

		recordingPosition.Clear();

		SpawnerHandler.Instance.AddGhost(temp); 

		startedRecording = false; 
	}

	void Update () 
	{
		if(!Input.GetMouseButton(0)) return; 

		NewMove();
	}


	Vector2 pointerDown; 
	Vector2 currentPoint; 
	float xVel = 0, yVel = 0; 
	void NewMove()
	{
		if(Input.GetMouseButtonDown(0))
		{
			pointerDown = Camera.main.ScreenToViewportPoint(Input.mousePosition); 

			hasPressedDown = true; 
		}

		if(!hasPressedDown) return; 

		currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition); 


		Vector2 rot = (Vector2)((Vector2)currentPosition - (Vector2)pointerDown); 

		transform.up = Vector2.Lerp(transform.up, rot, Time.deltaTime * 50f); 

		Vector2 locked = Vector2.ClampMagnitude((Vector2)((Vector2)currentPosition - (Vector2)pointerDown),1);

		float xMax = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x; 
		float xMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x; 

		float yMax = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y; 
		float yMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y; 

		Vector2 p = Camera.main.WorldToViewportPoint(transform.position); 

		if(p.x >= 0 && p.x <= 1)
		{
			xVel+=locked.x * Time.deltaTime * 50f; 
		}

		if(p.y >= 0 && p.y <= 1)
		{
			yVel+=locked.y * Time.deltaTime * 50f; 
		}


		xVel = Mathf.Clamp(xVel, xMin, xMax); 

		yVel = Mathf.Clamp(yVel, yMin, yMax); 

		transform.position = new Vector2(xVel, yVel); 


		if(!startedRecording)
		{
			StopCoroutine("IRecordPosition"); 
			StartCoroutine("IRecordPosition"); 
			startedRecording = true; 
		}
	}

	// void Move()
	// {
	// 	currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition); 

	// 	if(Input.GetMouseButtonDown(0))
	// 	{
	// 		lastPosition = currentPosition; 
	// 	}

	// 	float diffX = Mathf.Abs(currentPosition.x - lastPosition.x);
	// 	float diffY = Mathf.Abs(currentPosition.y - lastPosition.y); 



	// 	diffX = Mathf.Clamp(diffX, 0f, 20f) * 4f;

	// 	diffY = Mathf.Clamp(diffY, 0f, 20f) * 4f; 

	// 	if(currentPosition.x > lastPosition.x)
	// 	{
	// 		xPosition+= Time.deltaTime * diffX * senstivity; 
	// 	}
	// 	else if(currentPosition.x < lastPosition.x)
	// 	{
	// 		xPosition-= Time.deltaTime * diffX * senstivity;
	// 	}

	// 	if(currentPosition.y > lastPosition.y)
	// 	{

	// 		yPosition+= Time.deltaTime * diffY * senstivity; 
	// 	}
	// 	else if(currentPosition.y < lastPosition.y)
	// 	{
	// 		yPosition-= Time.deltaTime * diffY * senstivity;
	// 	}


	// 	float xMax = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x; 
	// 	float xMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x; 

	// 	float yMax = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y; 
	// 	float yMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y; 


	// 	xPosition = Mathf.Clamp(xPosition, xMin, xMax); 
	// 	yPosition = Mathf.Clamp(yPosition, yMin, yMax); 
	// 	transform.position = new Vector3(xPosition, yPosition, 0); 


	// 	lastPosition = currentPosition; 
	// }

	IEnumerator IRecordPosition()
	{
		recordingPosition.Clear(); 

		while(true)
		{
			recordingPosition.Add(transform.position); 
			yield return new WaitForSeconds(Time.deltaTime); 
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Ghost")
		{	
			if(EventManager.OnGameOver != null)
			{
				EventManager.OnGameOver();
			}
		}
	}
}

