using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightining : MonoBehaviour {

	private Light light;

	public float defaultIntensity = .3f;

	private float lightningTimer = 0;

	private float lightningCooldown = 3f;

	private bool started;

	private CameraController cameraController;

	void Start ()
	{
		light = transform.GetComponent<Light>();

		light.color = new Color(defaultIntensity, defaultIntensity, defaultIntensity);

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

		float duration = Random.Range(.5f, 1.0f);

		while (timer < duration)
		{
			timer += Time.deltaTime;

			intensity = Random.Range(0f, 1f);

			light.color = new Color(intensity, intensity, intensity);

			cameraController.Shake(CameraController.LIGHTNING_SHAKE);

			yield return new WaitForSeconds(Random.Range(.005f, .01f));
		}

		light.color = new Color(defaultIntensity, defaultIntensity, defaultIntensity);

		started = false;
	}
}
