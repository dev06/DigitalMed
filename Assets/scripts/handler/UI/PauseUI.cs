using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseUI : UserInterface {

	public override void Init()
	{
		base.Init();
	}
	public void Start()
	{
		Toggle(GameController.State == state);
	}

	void OnEnable ()
	{
		EventManager.OnStateChange += OnStateChange;
	}

	void OnDisable ()
	{
		EventManager.OnStateChange -= OnStateChange;
	}

	void OnStateChange(State s)
	{

		if (s != State.Pause)
		{
			Toggle(false);
			return;
		}

		Toggle(true);
	}

	public void Continue()
	{
		Time.timeScale = 1;
		GameController.Instance.SetState(State.Game);
	}

	public void Menu()
	{
		Time.timeScale = 1;
		if (EventManager.OnGameOver != null)
		{
			EventManager.OnGameOver();
		}
	}
}
