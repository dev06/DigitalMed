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
		EventManager.OnStateChange += OnStateChange;
	}
	void OnDisable()
	{
		EventManager.OnLevelComplete -= OnLevelComplete;
		EventManager.OnStateChange -= OnStateChange;
	}

	void Start ()
	{
		foreground = transform.GetChild(1).GetComponent<Image>();
		idol = FindObjectOfType<Checkpoint>();
		targetFill = idol.Power / 100f;
	}

	void OnStateChange(State s)
	{
		if (s == State.Game)
		{
			targetFill = idol.Power / 100f;
		}
	}



	void Update()
	{
		foreground.fillAmount = Mathf.SmoothDamp(foreground.fillAmount, targetFill, ref fillDamp, Time.deltaTime);
	}

	void OnLevelComplete()
	{
		idol.Power -= 20f;
		targetFill = idol.Power / 100f;
	}
}
