using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : UserInterface {

	public Animation hurtFlashAnim;

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
		EventManager.OnHitGhost += OnHitGhost;

		EventManager.OnStateChange += OnStateChange;
	}

	void OnDisable ()
	{
		EventManager.OnHitGhost -= OnHitGhost;

		EventManager.OnStateChange -= OnStateChange;
	}

	void OnStateChange(State s)
	{

		if (s != State.Game)
		{
			Toggle(false);
			return;
		}

		Toggle(true);
	}

	void OnHitGhost()
	{
		hurtFlashAnim.Play();
	}
}
