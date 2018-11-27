using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public const float LIGHTNING_SHAKE = .1F;

	private Vector3 defaultPosition;

	private float cameraShakeIntensity;

	private float cameraShakeWearoff;

	private Vector3 targetPosition;

	public float idolShakeIntensity = 4f;
	public float idolShakeWearOff = 14f;

	void OnEnable()
	{
		EventManager.OnCheckpointHit += OnCheckpointHit;
	}
	void OnDisable()
	{
		EventManager.OnCheckpointHit -= OnCheckpointHit;
	}

	void OnCheckpointHit()
	{
		Shake(idolShakeIntensity, idolShakeWearOff);
	}
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			Shake(8f, 14f);
		}
		cameraShakeIntensity -= Time.deltaTime * cameraShakeWearoff;
		cameraShakeIntensity = Mathf.Clamp(cameraShakeIntensity, 0, cameraShakeIntensity);
		transform.position = Vector3.Lerp(transform.position, targetPosition + GenerateShake(), Time.deltaTime * 10f);
	}

	private Vector3 GenerateShake()
	{
		return Random.insideUnitCircle * cameraShakeIntensity;
	}

	public void Shake(float intensity, float shakeWearOff = 1f)
	{
		this.cameraShakeIntensity = intensity;
		this.cameraShakeWearoff = shakeWearOff;
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
