using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public ParticleSystem hurtParticles;

	public List<Key> keysCollected = new List<Key>();

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

	private Vector3 startingPosition = new Vector3(-12, 0, -12);

	private bool lockMove;

	private Vector3 velocity;

	void OnEnable()
	{
		EventManager.OnCheckpointHit += OnCheckpointHit;

		EventManager.OnLevelComplete += OnLevelComplete;
	}
	void OnDisable()
	{
		EventManager.OnCheckpointHit -= OnCheckpointHit;

		EventManager.OnLevelComplete -= OnLevelComplete;
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

		Camera.main.transform.GetComponent<CameraController>().SetPosition(transform.position + defaultPosition);

		speed = 0f;

		animator.SetBool("isWalking", Input.GetMouseButton(0) && !lockMove && velocity != Vector3.zero);

		if (lockMove)
		{
			return;
		}

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

		velocity = currentPointer - pointerDown;


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

		float mapSize = 31f;

		posX = Mathf.Clamp(posX, -mapSize * .5f, mapSize * .5f);

		posZ = Mathf.Clamp(posZ, -mapSize * .5f, mapSize * .5f);

		transform.position = new Vector3(posX, defaultYPos, posZ);

		if (v != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(v, Vector3.up);
		}

		lastPosition = transform.position;

	}

	private void OnLevelComplete()
	{

		transform.position = startingPosition;

		lastPosition = startingPosition;

		transform.rotation = Quaternion.Euler(Vector3.zero);
		//	currentPointer = pointerDown = Vector3.zero;

		StopCoroutine("IDisableLockMove");

		StartCoroutine("IDisableLockMove");

		recordingPosition.Clear();
	}

	private IEnumerator IDisableLockMove()
	{
		yield return new WaitForSeconds(2f);
		LockMove = false;
	}

	public bool LockMove
	{
		get { return lockMove;}
		set {this.lockMove = value;}
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
			if (!GameplayController.Instance.CanDie) { return; }

			Health--;

			hurtParticles.Play();

			Health = Mathf.Clamp(health, 0, health);

			if (Health <= 0)
			{

				HidePlayer();

				GameController.Instance.SetState(State.GameOver);
				// if (EventManager.OnGameOver != null)
				// {
				// 	EventManager.OnGameOver();
				// }
			}

			if (EventManager.OnHitGhost != null)
			{
				EventManager.OnHitGhost();
			}
		}

		if (col.gameObject.tag == "Key")
		{
			AddKey(col.gameObject.transform.GetComponent<Key>());
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "ScrollPost")
		{
			if (EventManager.OnScrollPostHit != null)
			{
				EventManager.OnScrollPostHit();
			}
		}
	}

	private void HidePlayer()
	{
		transform.gameObject.SetActive(false);
		Camera.main.transform.GetComponent<CameraController>().Shake(8f, 14f);
		// GetComponent<MeshRenderer>().enabled = false;
		// GetComponent<BoxCollider>().enabled = false;
	}

	public void MovePlayerToStartPosition()
	{
		transform.position = defaultPosition;
	}

	public int Health
	{
		get { return health;}
		set {this.health = value;}
	}

	public void AddKey(Key key)
	{
		keysCollected.Add(key);
	}

	public bool ContainsKey(Key key)
	{
		for (int i = 0; i < keysCollected.Count; i++)
		{
			if (keysCollected[i] == key)
			{
				return true;
			}
		}

		return false;
	}
}
