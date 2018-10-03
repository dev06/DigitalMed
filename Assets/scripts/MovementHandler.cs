

// Attached to player. Class used for player movement and reording.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementHandler : MonoBehaviour 
{

	private Vector2 currentPosition, lastPosition;
	private float xPosition, yPosition;
	private float senstivity = 10f;
	private bool startedRecording;
	private List<Vector2> recordingPosition = new List<Vector2>();
	private bool hasPressedDown;
	private TrailRenderer trail;
	private Vector2 pointerDown;
	private Vector2 currentPoint;
	private Vector2 lPosition;
	private Vector2 _origPos;
	private Camera mainCamera; 


	void OnEnable()
	{
		EventManager.OnCheckpointHit += OnCheckpointHit;
	}
	void OnDisable()
	{
		EventManager.OnCheckpointHit -= OnCheckpointHit;
	}

	void Start()
	{
		//trail = GetComponentInChildren<TrailRenderer>();
		//trail.endWidth = 0;
		pointerDown = Vector2.zero;
		currentPosition = Vector2.zero;
		_origPos = (Vector2)transform.position;
		mainCamera = Camera.main; 

	}

	//Method called when player hits checkpoints. 
	void OnCheckpointHit()
	{
		StopCoroutine("IRecordPosition");

		List<Vector2> temp = new List<Vector2>();

		for (int i = 0; i < recordingPosition.Count; i++)
		{
			temp.Add(recordingPosition[i]);
		}

		recordingPosition.Clear();

		//SpawnerHandler.Instance.AddGhost(temp);

		startedRecording = false;
	}


	void Update ()
	{	
		Vector3 targetPosition = transform.position + new Vector3(0, 0, -10f); 

		mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * 10f); 


		if(Input.GetMouseButtonDown(0))
		{
			pointerDown = Camera.main.ScreenToViewportPoint(Input.mousePosition); 
		}

		if (!Input.GetMouseButton(0)) { return; }

		Move();

		Vector2 moveDirection = (Vector2)gameObject.transform.position - _origPos;

		if (moveDirection != Vector2.zero)
		{

			transform.up = moveDirection;
		}
	}

	void Move()
	{
		currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition); 

		Vector2 v = currentPosition - pointerDown;

		v.Normalize(); 

		if (!startedRecording)
		{
			StopCoroutine("IRecordPosition");
			StartCoroutine("IRecordPosition");
			startedRecording = true;
		}


		transform.Translate(v * Time.deltaTime * senstivity, Space.World);

		float xPos = transform.position.x; 
		float yPos = transform.position.y; 

		xPos = Mathf.Clamp(xPos, -10f, 10f); 
		yPos = Mathf.Clamp(yPos, -10f, 10f); 

		transform.position = new Vector2(xPos, yPos); 


	}

	//Records the player the Position
	IEnumerator IRecordPosition()
	{
		recordingPosition.Clear();

		while (true)
		{
			recordingPosition.Add(transform.position);

			yield return null;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.tag == "Ghost")
		{
			if (EventManager.OnGameOver != null)
			{
				EventManager.OnGameOver();
			}
		}
	}
}


