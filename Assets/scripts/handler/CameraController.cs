using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public const float LIGHTNING_SHAKE = .1F;

	private Vector3 defaultPosition;

	private float cameraShakeIntensity;

	private Vector3 targetPosition;

	void Start ()
	{

		//defaultPosition = transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			Shake(0.4f);
		}
		cameraShakeIntensity -= Time.deltaTime;
		cameraShakeIntensity = Mathf.Clamp(cameraShakeIntensity, 0, cameraShakeIntensity);
		transform.position = targetPosition + GenerateShake();
	}

	private Vector3 GenerateShake()
	{
		return Random.insideUnitCircle * cameraShakeIntensity;
	}

	public void Shake(float intensity)
	{
		this.cameraShakeIntensity = intensity;
	}

	public void SetPosition(Vector3 position)
	{
		targetPosition = position;
	}

	public void FlashBloom()
	{
		GetComponent<Animation>().Play();
	}
}
