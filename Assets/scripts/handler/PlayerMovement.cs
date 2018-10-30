using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private int health = 3;
	private Vector2 pointerDown;
	private Vector2 currentPointer;
	private Rigidbody rigidbody;
	private Vector3 lastPosition;
	private Vector3 defaultPosition;

	private List<Vector3> recordingPosition = new List<Vector3>();
	private bool startedRecording;
	private float defaultYPos;
	private float speed = 0;

	private Animator animator;


	public ParticleSystem hurtParticles;



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
		animator = GetComponent<Animator>();
	}


	void Update ()
	{

		// Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position  + defaultPosition, Time.deltaTime * 10f);

		Camera.main.transform.GetComponent<CameraController>().SetPosition(transform.position + defaultPosition);

		speed = 0f;

		animator.SetBool("isWalking", Input.GetMouseButton(0));


		if (!Input.GetMouseButton(0)) { return; }

		if (Input.GetMouseButtonUp(0))
		{
			pointerDown = Vector2.zero;
			return;
		}

		Move();
	}

	void Move()
	{
		speed = 8;

		if (Input.GetMouseButtonDown(0))
		{
			pointerDown = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}



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


		transform.Translate(v * Time.deltaTime * speed, Space.World);

		float posX = transform.position.x;
		float posZ = transform.position.z;

		posX = Mathf.Clamp(posX, -12f, 12f);
		posZ = Mathf.Clamp(posZ, -12f, 12f);

		transform.position = new Vector3(posX, defaultYPos, posZ);

		if (v != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(v, Vector3.up);
		}

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

		if (GameplayController.Instance.collectedAllCheckpoints == false)
		{
		}
		SpawnerHandler.Instance.AddGhost(temp);


		startedRecording = false;
	}


	void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.tag == "Ghost")
		{
			if (!GameplayController.Instance.CanDie) { return; }

			Health--;

			hurtParticles.Play();

			Health = Mathf.Clamp(health, 0, health);

			if (Health <= 0)
			{
				if (EventManager.OnGameOver != null)
				{
					EventManager.OnGameOver();
				}
			}

			if (EventManager.OnHitGhost != null)
			{
				EventManager.OnHitGhost();
			}
		}
	}

	public int Health
	{
		get { return health;}
		set {this.health = value;}
	}
}
