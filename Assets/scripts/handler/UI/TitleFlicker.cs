using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleFlicker : MonoBehaviour {

	public Shadow shadow;
	public float min;
	public float max;
	public float delay;
	private float timer;

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > delay)
		{
			shadow.effectDistance = new Vector2(0, Random.Range(min, max));
			timer = 0;
		}
	}
}
