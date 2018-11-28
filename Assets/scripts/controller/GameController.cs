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
	GameOver,
	Settings,
	Credits,
}

public class GameController : MonoBehaviour {

	public bool DeleteSave;

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

	void OnValidate()
	{
		if (DeleteSave)
		{
			PlayerPrefs.DeleteAll();
		}
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

		PlayerPrefs.SetFloat("POWER", FindObjectOfType<Checkpoint>().Power);
	}

	private void Load()
	{
		State = State.Menu;

		LevelController.CURRENT_LEVEL = PlayerPrefs.HasKey("CURRENT_LEVEL") ? PlayerPrefs.GetInt("CURRENT_LEVEL") : 0;

		FindObjectOfType<Checkpoint>().Power = PlayerPrefs.HasKey("POWER") ? PlayerPrefs.GetFloat("POWER") : 100;
	}

	void OnGameOver()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
