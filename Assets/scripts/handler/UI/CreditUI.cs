using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreditUI : UserInterface {

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

		if (s != State.Credits)
		{
			Toggle(false);
			return;
		}

		Toggle(true);
	}
}
