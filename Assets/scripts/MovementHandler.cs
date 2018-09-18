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
		xVel = 0;
		yVel = 0;

		_origPos = (Vector2)transform.position;

	}

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

	Vector2 lPosition;
	Vector2 _origPos;
	void Update ()
	{


		if (!Input.GetMouseButton(0)) { return; }

		Move();

		Vector2 moveDirection = (Vector2)gameObject.transform.position - _origPos;
		if (moveDirection != Vector2.zero)
		{

			transform.up = moveDirection;
		}
		//Move_3();
	}

	void Move_3()
	{


		if (Input.GetMouseButton(0))
		{
			if (Input.GetMouseButtonDown(0))
			{
				pointerDown = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			}

			currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

			Vector2 locked = Vector2.ClampMagnitude((Vector2)((Vector2)currentPosition - (Vector2)pointerDown), 1);

			float magnitude = pointerDown.x - currentPosition.x;
			transform.Rotate(Vector3.forward, Time.deltaTime * magnitude * 360f);
			Vector2 rot = (Vector2)((Vector2)currentPosition - (Vector2)pointerDown);

			if (!startedRecording)
			{
				StopCoroutine("IRecordPosition");
				StartCoroutine("IRecordPosition");
				startedRecording = true;
			}
		}


		transform.Translate(transform.up * Time.deltaTime * 5f, Space.World);


	}


	Vector2 pointerDown;
	Vector2 currentPoint;
	float xVel = 0, yVel = 0;
	void NewMove()
	{
		if (Input.GetMouseButtonDown(0))
		{
			pointerDown = Camera.main.ScreenToViewportPoint(Input.mousePosition);

			hasPressedDown = true;
		}

		if (!hasPressedDown) { return; }

		currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);


		Vector2 rot = (Vector2)((Vector2)currentPosition - (Vector2)pointerDown);

		transform.up = Vector2.Lerp(transform.up, rot, Time.deltaTime * 50f);

		Vector2 locked = Vector2.ClampMagnitude((Vector2)((Vector2)currentPosition - (Vector2)pointerDown), 2);

		float xMax = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
		float xMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;

		float yMax = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
		float yMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;

		Vector2 p = Camera.main.WorldToViewportPoint(transform.position);

		if (p.x >= 0 && p.x <= 1)
		{
			xVel += locked.x * Time.deltaTime * 10f;
		}

		if (p.y >= 0 && p.y <= 1)
		{
			yVel += locked.y * Time.deltaTime * 10f;
		}


		xVel = Mathf.Clamp(xVel, xMin, xMax);

		yVel = Mathf.Clamp(yVel, yMin, yMax);

		transform.position = new Vector2(xVel, yVel);


		if (!startedRecording)
		{
			StopCoroutine("IRecordPosition");
			StartCoroutine("IRecordPosition");
			startedRecording = true;
		}
	}

	float speed = 1;
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

