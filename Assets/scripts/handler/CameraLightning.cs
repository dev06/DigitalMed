using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLightning : MonoBehaviour {

	private Camera camera;

	public Animation powerbeamFlash;

	public float defaultIntensity = .3f;

	private float lightningTimer = 0;

	private float lightningCooldown = 1f;

	private bool started;

	private CameraController cameraController;

	void OnEnable()
	{
		EventManager.OnPowerbeamStruck += OnPowerbreamStruck;
	}
	void OnDisable()
	{
		EventManager.OnPowerbeamStruck -= OnPowerbreamStruck;
	}


	void Start ()
	{
		camera = transform.GetComponent<Camera>();

		camera.backgroundColor = new Color(defaultIntensity, defaultIntensity, defaultIntensity);

		cameraController = FindObjectOfType<CameraController>();
	}

	void Update ()
	{
		if (started) { return; }

		lightningTimer += Time.deltaTime;

		if (lightningTimer > lightningCooldown)
		{
			StopCoroutine("StartLightining");

			StartCoroutine("StartLightining");

			started = true;

			lightningTimer = 0;
		}
	}

	IEnumerator StartLightining()
	{
		lightningCooldown = Random.Range(5f, 10f);

		float timer = 0;

		float intensity = 0;

		float duration = Random.Range(0f, 1f);

		while (timer < duration)
		{
			timer += Time.deltaTime;

			intensity = Random.Range(.2f, .3f);

			camera.backgroundColor = new Color(intensity, intensity, intensity);

			//cameraController.Shake(CameraController.LIGHTNING_SHAKE);

			yield return new WaitForSeconds(Random.Range(.005f, .01f));
		}

		camera.backgroundColor = new Color(defaultIntensity, defaultIntensity, defaultIntensity);

		started = false;
	}


	private void OnPowerbreamStruck()
	{
		GetComponent<Animation>().Play();

		powerbeamFlash.Play();
	}
}
