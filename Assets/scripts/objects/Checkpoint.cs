using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Checkpoint : MonoBehaviour {

	public ParticleSystem energyDraw;

	public AudioSource sfx;

	private float power = 100f;

	private PlayerMovement movementHandler;

	private CameraController cameraController;

	private Vector3 targetPosition;

	private SpawnerHandler spawner;

	private Transform ghostContainer;

	private LineRenderer lineRenderer;

	private ParticleSystem lightingBolt;

	private bool isHovering;




	void OnEnable()
	{
		EventManager.OnLevelComplete += OnLevelComplete;
		EventManager.OnStartHoverIdol += OnStartHoverIdol;
	}
	void OnDisable()
	{
		EventManager.OnLevelComplete -= OnLevelComplete;
		EventManager.OnStartHoverIdol -= OnStartHoverIdol;
	}

	public void Init ()
	{
		movementHandler = FindObjectOfType<PlayerMovement>();

		cameraController = FindObjectOfType<CameraController>();

		spawner = FindObjectOfType<SpawnerHandler>();

		targetPosition = GetNextLocation();

		ghostContainer = GameObject.FindWithTag("Containers/Ghost").transform;

		lineRenderer = transform.GetComponentInChildren<LineRenderer>();

		lightingBolt = transform.GetChild(4).GetComponent<ParticleSystem>();

		lineRenderer.enabled = false;

		Toggle(true);
	}

	private void Toggle(bool b)
	{
		GetComponent<MeshRenderer>().enabled = b;
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine("IHover");
		}

		if (!isHovering)
		{
			transform.position = Vector3.Lerp(transform.position, targetPosition + new Vector3(0, .75f + Mathf.PingPong(Time.time * .5f, 1f) - .5f, 0), Time.deltaTime * 10f);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Player" || isHovering) { return; }

		if (EventManager.OnCheckpointHit != null)
		{
			EventManager.OnCheckpointHit();
		}

		PlaySFX(AppResources.swish_1);

		if (GameplayController.Instance.checkpointsCollected < LevelController.Instance.CurrentLevel.CheckpointCount)
		{
			targetPosition = GetNextLocation();
		}

	}

	private Vector3 GetNextLocation()
	{
		Vector3 location = Vector3.up;

		Transform currentLevelObjectLocations = LevelController.Instance.CurrentLevel.CheckpointContainer;

		location = currentLevelObjectLocations.GetChild(GameplayController.Instance.checkpointsCollected).position;

		//Debug.Log(LevelController.Instance.CurrentLevel + " " + location + " " + GameplayController.Instance.checkpointsCollected);
		return location;
	}

	private void OnStartHoverIdol()
	{
		StopCoroutine("IHover");
		StartCoroutine("IHover");
	}


	private IEnumerator IHover()
	{
		isHovering = true;

		Vector3 currentPos = transform.position;

		float heightOffset = 0;

		float targetHeight = 5f;

		float speedMultiplier = 2f;

		while (heightOffset < targetHeight)
		{
			heightOffset += Time.deltaTime * (targetHeight - heightOffset) * speedMultiplier;

			if (heightOffset > targetHeight - .2f )
			{
				break;
			}

			transform.position = new Vector3(transform.position.x, heightOffset, transform.position.z);

			yield return null;
		}

		float shakeTimer = 0;

		Vector3 hoveringPos = transform.position;

		energyDraw.Play();

		while (shakeTimer < 2f)
		{
			shakeTimer += Time.deltaTime;

			transform.position = Vector3.Lerp(transform.position, hoveringPos + (Vector3)(Random.insideUnitCircle * .1f), Time.deltaTime * 10f);

			yield return new WaitForSeconds(Time.deltaTime);
		}

		energyDraw.Stop();

		//lineRenderer.enabled = true;

		//lineRenderer.endWidth = .1f;

		//lineRenderer.SetPosition(0, transform.position + new Vector3(0, 2f, 0));

		lightingBolt.Play();

		for (int i = 0; i < ghostContainer.childCount; i++)
		{
			if (!ghostContainer.GetChild(i).gameObject.activeSelf) { continue; }

			lightingBolt.transform.position = ghostContainer.GetChild(i).transform.position + new Vector3(0, -1f, 0);

			//lineRenderer.SetPosition(1, ghostContainer.GetChild(i).transform.position);

			ghostContainer.GetChild(i).GetComponent<Ghost>().Toggle(false);

			if (EventManager.OnPowerbeamStruck != null)
			{
				EventManager.OnPowerbeamStruck();
			}

			PlaySFX(AppResources.thunder_zap);


			cameraController.FlashBloom();

			//	yield return new WaitForSeconds(.31f);

			//lineRenderer.enabled = false;

			lightingBolt.Stop();

			yield return new WaitForSeconds(.31f);

			//lineRenderer.enabled = true;

			lightingBolt.Play();
		}

		lightingBolt.Stop();

		shakeTimer = 0;

		FindObjectOfType<PowerbeamFlash>().IncreaseFade();

		float shakeIntensity = 0f;

		// while (shakeTimer < 1f)
		// {
		// 	shakeTimer += Time.deltaTime;

		// 	shakeIntensity = shakeTimer;

		// 	shakeIntensity = Mathf.Clamp(shakeIntensity, 0, .4f);

		// 	transform.position = Vector3.Lerp(transform.position, hoveringPos + (Vector3)(Random.insideUnitCircle * shakeIntensity), Time.deltaTime * 10f);

		// 	yield return new WaitForSeconds(Time.deltaTime);
		// }

		targetPosition = transform.position;

		isHovering = false;

		Debug.Log("Out");

	}

	void OnLevelComplete()
	{
		GameplayController.Instance.checkpointsCollected = 0;

		StopCoroutine("IWait");
		StartCoroutine("IWait");
	}

	IEnumerator IWait()
	{
		yield return new WaitForSeconds(.25f);
		targetPosition = GetNextLocation();
	}

	public float Power
	{
		get { return power; }
		set {this.power = value; }
	}


	void PlaySFX(AudioClip clip)
	{
		sfx.clip = clip;
		sfx.Play();
	}

}
