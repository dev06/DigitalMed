using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelProgression : MonoBehaviour {

	private Image foreground;

	private Checkpoint idol;

	private float targetFill, fillDamp;

	void OnEnable()
	{
		EventManager.OnLevelComplete += OnLevelComplete;
	}
	void OnDisable()
	{
		EventManager.OnLevelComplete -= OnLevelComplete;
	}

	void Start ()
	{
		foreground = transform.GetChild(1).GetComponent<Image>();
		idol = FindObjectOfType<Checkpoint>();
		targetFill = 1f;
	}

	void Update()
	{
		foreground.fillAmount = Mathf.SmoothDamp(foreground.fillAmount, targetFill, ref fillDamp, Time.deltaTime);
	}

	void OnLevelComplete()
	{
		idol.Power -= 25f;
		targetFill = idol.Power / 100f;
	}
}
