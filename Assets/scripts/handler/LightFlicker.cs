using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

	private Light spotLight;
	private float timer;
	private float flickerRate = .1f;
	[TooltipAttribute("Range of values it will choose from 0 - 1. Lower equals more blacker")]
	public float range = 1f;
	void Start ()
	{
		spotLight = GetComponent<Light>();
	}

	void Update ()
	{
		timer += Time.deltaTime;
		if (timer > flickerRate)
		{
			FlickerLight();
			timer = 0;
		}
	}

	private void FlickerLight()
	{
		float value = Random.Range(0f, range);
		spotLight.color = new Color(value, value, value, 1);
	}
}
