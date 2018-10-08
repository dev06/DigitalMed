using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController Instance; 


	void OnEnable()
	{
		EventManager.OnGameOver+=OnGameOver; 
	}
	void OnDisable()
	{
		EventManager.OnGameOver-=OnGameOver; 
	}

	void Awake()
	{
		Application.targetFrameRate = 60;

		if(Instance == null)
		{
			Instance = this; 
		}
	}

	void OnGameOver()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0); 
	}
}
