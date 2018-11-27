using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOver : UserInterface {


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
		if (s != state)
		{
			Toggle(false);
			return;
		}
		Toggle(true);
	}

	void Update()
	{
		if (GameController.State != state) { return; }

		if (Input.GetMouseButtonDown(0))
		{
			if (EventManager.OnGameOver != null)
			{
				EventManager.OnGameOver();
			}
		}
	}

}
