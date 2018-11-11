using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : UserInterface {



	void OnEnable () {
		EventManager.OnStateChange += OnStateChange;
	}


	void OnDisable () {
		EventManager.OnStateChange -= OnStateChange;
	}

	public void Start()
	{
		Toggle(GameController.State == state);
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

	public override void Init()
	{
		base.Init();
	}

}
