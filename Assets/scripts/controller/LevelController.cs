using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController Instance;

	public static int CURRENT_LEVEL = 0;

	private LevelObjects _levelObjects;

	public Transform _currentLevelObject;


	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			DestroyImmediate(gameObject);
		}

		CURRENT_LEVEL = 0;
	}


	void OnEnable()
	{
		EventManager.OnLevelComplete += OnLevelComplete;
	}

	void OnDisable()
	{
		EventManager.OnLevelComplete -= OnLevelComplete;
	}

	void OnLevelComplete()
	{
		IncrementLevel();

		_levelObjects.ToggleLevelObject(CURRENT_LEVEL);
	}

	void IncrementLevel()
	{
		CURRENT_LEVEL++;
	}

	void Start ()
	{
		_levelObjects = FindObjectOfType<LevelObjects>();
	}

	void Update ()
	{

	}

	public Transform CurrentLevelObject
	{
		set {this._currentLevelObject = value; }
		get {return _currentLevelObject; }
	}
}
