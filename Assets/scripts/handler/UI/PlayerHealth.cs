using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour {

	private PlayerMovement player;

	void OnEnable()
	{
		EventManager.OnHitGhost += OnHitGhost;
	}

	void OnDisable()
	{
		EventManager.OnHitGhost -= OnHitGhost;
	}

	void Start ()
	{
		player = FindObjectOfType<PlayerMovement>();
	}

	void OnHitGhost()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}

		for (int i = 0; i < player.Health; i++)
		{
			transform.GetChild(i).gameObject.SetActive(true);
		}

	}

	void Update () {

	}


}
