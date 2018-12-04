using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugMode : MonoBehaviour {

	public static bool ACTIVE = true;
	public Sprite debug_on;
	public Sprite debug_off;

	public Image debug_image;

	void Start ()
	{
		UpdateSprite();
	}


	void Update () {

	}

	public void UpdateSprite()
	{
		debug_image.sprite = ACTIVE ? debug_on : debug_off;
	}

	public void Toggle()
	{
		ACTIVE = !ACTIVE;
		UpdateSprite();
	}

	public void NextLevel()
	{
		if (EventManager.OnStartHoverIdol != null)
		{
			EventManager.OnStartHoverIdol();
		}
	}
}
