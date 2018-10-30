using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : MonoBehaviour {

	public Animation hurtFlashAnim;


	void OnEnable ()
	{
		EventManager.OnHitGhost += OnHitGhost;
	}

	void OnDisable ()
	{
		EventManager.OnHitGhost -= OnHitGhost;
	}

	void OnHitGhost()
	{
		hurtFlashAnim.Play();
	}
}
