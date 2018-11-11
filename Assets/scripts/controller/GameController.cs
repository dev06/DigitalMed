using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	None,
	Menu,
	Game,
	Pause,
	Message,
	Tutorial,
}

public class GameController : MonoBehaviour {

	public static GameController Instance;

	public static State State = State.Menu;

	void OnEnable()
	{
		EventManager.OnGameOver += OnGameOver;

		EventManager.OnLevelComplete += OnLevelComplete;
	}
	void OnDisable()
	{
		EventManager.OnGameOver -= OnGameOver;

		EventManager.OnLevelComplete -= OnLevelComplete;
	}

	void Awake()
	{
		Application.targetFrameRate = 60;

		if (Instance == null)
		{
			Instance = this;
		}

		Load();
	}

	public void SetState(State s)
	{
		State = s;

		if (EventManager.OnStateChange != null)
		{
			EventManager.OnStateChange(State);
		}
	}

	void OnLevelComplete()
	{
		StopCoroutine("IDelay");
		StartCoroutine("IDelay");
	}

	IEnumerator IDelay()
	{
		yield return new WaitForSeconds(.1f);

		PlayerPrefs.SetInt("CURRENT_LEVEL", LevelController.CURRENT_LEVEL);
	}

	private void Load()
	{
		State = State.Menu;
		LevelController.CURRENT_LEVEL = PlayerPrefs.HasKey("CURRENT_LEVEL") ? PlayerPrefs.GetInt("CURRENT_LEVEL") : 0;
	}

	void OnGameOver()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
