using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuUI : UserInterface
{

	public Text startText;

	void OnEnable () {
		EventManager.OnStateChange += OnStateChange;
	}


	void OnDisable () {
		EventManager.OnStateChange -= OnStateChange;
	}


	public override void Init()
	{
		base.Init();
	}

	public void Start()
	{
		if (PlayerPrefs.HasKey("CURRENT_LEVEL"))
		{
			if (PlayerPrefs.GetInt("CURRENT_LEVEL") > 0)
			{
				startText.text = "Continue";
			}
		}
		else
		{
			startText.text = "New Game";
		}

		Toggle(GameController.State == state);
	}

	void OnStateChange(State s)
	{
		if (s != State.Menu)
		{
			Toggle(false);
			return;
		}

		Toggle(true);
	}

	public void StartGame()
	{
		State s = LevelController.CURRENT_LEVEL == 0 ? State.Tutorial : State.Game;

		GameController.Instance.SetState(s);
	}

	public void ActivateSettings()
	{
		GameController.Instance.SetState(State.Settings);
	}

	public void ActiveCredits()
	{
		GameController.Instance.SetState(State.Credits);

	}

}
