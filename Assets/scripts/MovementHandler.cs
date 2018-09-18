

// Attached to player. Class used for player movement and reording.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementHandler : MonoBehaviour 
{

	private Vector3 currentPosition, lastPosition;
	private float xPosition, yPosition;
	private float senstivity = 250f;
	private bool startedRecording;
	private List<Vector2> recordingPosition = new List<Vector2>();
	private bool hasPressedDown;
	private TrailRenderer trail;
	private Vector2 pointerDown;
	private Vector2 currentPoint;
	private Vector2 lPosition;
	private Vector2 _origPos;


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
		trail = GetComponentInChildren<TrailRenderer>();
		trail.endWidth = 0;
		pointerDown = Vector2.zero;
		currentPosition = Vector2.zero;
		_origPos = (Vector2)transform.position;

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

		SpawnerHandler.Instance.AddGhost(temp);

		startedRecording = false;
	}


	void Update ()
	{
		if (!Input.GetMouseButton(0)) { return; }

		Move();

		Vector2 moveDirection = (Vector2)gameObject.transform.position - _origPos;

		if (moveDirection != Vector2.zero)
		{

			transform.up = moveDirection;
		}
	}

	//Moves the player
	void Move()
	{
		currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
		{
			lastPosition = currentPosition;
		}

		float diffX = Mathf.Abs(currentPosition.x - lastPosition.x);
		float diffY = Mathf.Abs(currentPosition.y - lastPosition.y);



		diffX = Mathf.Clamp(diffX, 0f, 20f) * 8f;

		diffY = Mathf.Clamp(diffY, 0f, 20f) * 8f;

		if (currentPosition.x > lastPosition.x)
		{
			xPosition += Time.deltaTime * diffX * senstivity;
		}
		else if (currentPosition.x < lastPosition.x)
		{
			xPosition -= Time.deltaTime * diffX * senstivity;
		}

		if (currentPosition.y > lastPosition.y)
		{

			yPosition += Time.deltaTime * diffY * senstivity;
		}
		else if (currentPosition.y < lastPosition.y)
		{
			yPosition -= Time.deltaTime * diffY * senstivity;
		}


		float xMax = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
		float xMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;

		float yMax = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
		float yMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;


		xPosition = Mathf.Clamp(xPosition, xMin, xMax);
		yPosition = Mathf.Clamp(yPosition, yMin, yMax);
		transform.position = new Vector3(xPosition, yPosition, 0);

		if (!startedRecording)
		{
			StopCoroutine("IRecordPosition");
			StartCoroutine("IRecordPosition");
			startedRecording = true;
		}

		lastPosition = currentPosition;
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

	void OnTriggerEnter2D(Collider2D col)
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


