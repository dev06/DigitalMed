using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	private Vector2 pointerDown; 
	private Vector2 currentPointer; 
	private Rigidbody rigidbody; 
	private Vector3 lastPosition; 
	private Vector3 defaultPosition; 

	private List<Vector3> recordingPosition = new List<Vector3>();
	private bool startedRecording;
	private float defaultYPos;




	void OnEnable()
	{
		EventManager.OnCheckpointHit += OnCheckpointHit;
	}
	void OnDisable()
	{
		EventManager.OnCheckpointHit -= OnCheckpointHit;
	}

	void Start () 
	{
		rigidbody = transform.GetComponent<Rigidbody>();
		defaultPosition = Camera.main.transform.position; 
		defaultYPos = transform.position.y; 
	}


	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			pointerDown = Camera.main.ScreenToViewportPoint(Input.mousePosition); 
		}	

		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position  + defaultPosition, Time.deltaTime * 10f); 

		if(Input.GetMouseButton(0) == false) return; 

		Move(); 
	}

	void Move()
	{
		currentPointer = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		Vector3 velocity = currentPointer - pointerDown; 

		velocity.Normalize(); 

		Vector3 v = new Vector3(velocity.x, 0, velocity.y); 


		if (!startedRecording)
		{
			StopCoroutine("IRecordPosition");
			StartCoroutine("IRecordPosition");
			startedRecording = true;
		}


		transform.Translate(v * Time.deltaTime * 10f, Space.World); 

		transform.position = new Vector3(transform.position.x, defaultYPos, transform.position.z); 

		transform.rotation = Quaternion.LookRotation(v, Vector3.up); 

		lastPosition = transform.position; 

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


	void OnCheckpointHit()
	{
		StopCoroutine("IRecordPosition");

		List<Vector3> temp = new List<Vector3>();

		for (int i = 0; i < recordingPosition.Count; i++)
		{
			temp.Add(recordingPosition[i]);
		}

		recordingPosition.Clear();

		SpawnerHandler.Instance.AddGhost(temp);

		startedRecording = false;
	}


	void OnTriggerEnter(Collider col)
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
