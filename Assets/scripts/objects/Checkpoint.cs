﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Checkpoint : MonoBehaviour {

	public ParticleSystem energyDraw;

	PlayerMovement movementHandler;

	CameraController cameraController;

	Vector3 targetPosition;

	private SpawnerHandler spawner;

	private Transform ghostContainer;

	private LineRenderer lineRenderer;


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

		transform.position = GetNextLocation();

		ghostContainer = GameObject.FindWithTag("Containers/Ghost").transform;

		lineRenderer = transform.GetComponentInChildren<LineRenderer>();

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
		//transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Player") { return; }

		if (EventManager.OnCheckpointHit != null)
		{
			EventManager.OnCheckpointHit();
		}

		Debug.Log(GameplayController.Instance.checkpointsCollected  + " " + LevelController.Instance.CurrentLevel.CheckpointCount);

		if (GameplayController.Instance.checkpointsCollected < LevelController.Instance.CurrentLevel.CheckpointCount)
		{
			transform.position = GetNextLocation();
		}

	}

	private Vector3 GetNextLocation()
	{
		Vector3 location = Vector3.up;

		Transform currentLevelObjectLocations = LevelController.Instance.CurrentLevel.CheckpointContainer;

		location = currentLevelObjectLocations.GetChild(GameplayController.Instance.checkpointsCollected).position;

		return location;
	}

	private void OnStartHoverIdol()
	{
		StopCoroutine("IHover");
		StartCoroutine("IHover");
	}


	private IEnumerator IHover()
	{
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

			transform.position = hoveringPos + (Vector3)(Random.insideUnitCircle * .1f);

			yield return new WaitForSeconds(Time.deltaTime);
		}

		energyDraw.Stop();

		lineRenderer.enabled = true;

		lineRenderer.endWidth = .1f;

		lineRenderer.SetPosition(0, transform.position + new Vector3(0, 2f, 0));

		for (int i = 0; i < ghostContainer.childCount; i++)
		{
			if (!ghostContainer.GetChild(i).gameObject.activeSelf) { continue; }

			lineRenderer.SetPosition(1, ghostContainer.GetChild(i).transform.position);

			ghostContainer.GetChild(i).GetComponent<Ghost>().Toggle(false);

			if (EventManager.OnPowerbeamStruck != null)
			{
				EventManager.OnPowerbeamStruck();
			}

			cameraController.FlashBloom();

			yield return new WaitForSeconds(.31f);

			lineRenderer.enabled = false;

			yield return new WaitForSeconds(.31f);

			lineRenderer.enabled = true;

		}

		lineRenderer.enabled = false;

		shakeTimer = 0;

		FindObjectOfType<PowerbeamFlash>().IncreaseFade();

		float shakeIntensity = 0f;

		while (shakeTimer < 4f)
		{
			shakeTimer += Time.deltaTime;

			shakeIntensity = shakeTimer;

			shakeIntensity = Mathf.Clamp(shakeIntensity, 0, .4f);

			transform.position = hoveringPos + (Vector3)(Random.insideUnitCircle * shakeIntensity);

			yield return new WaitForSeconds(Time.deltaTime);
		}



		Debug.Log("Out");

	}

	void OnLevelComplete()
	{
		transform.position = GetNextLocation();
	}
}
