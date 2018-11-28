using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHandler : MonoBehaviour {

	private static TrackHandler Instance; 

	public List<AudioClip> clips = new List<AudioClip>();

	public AudioSource gameTrack;

	private int clipIndex;

	void OnEnable()
	{
		EventManager.OnStateChange += OnStateChange;
		UnityEngine.SceneManagement.SceneManager.sceneLoaded+=OnSceneLoaded; 
	}

	void OnDisable()
	{
		EventManager.OnStateChange -= OnStateChange;
		UnityEngine.SceneManagement.SceneManager.sceneLoaded-=OnSceneLoaded; 
	}

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this; 
		}

		if(Instance != this)
		{
			DestroyImmediate(gameObject); 
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}

	}

	void Start () 
	{
		Play(clips[clipIndex]);
	}

	void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
	{
		if(scene.name == "final")
		{
			PlayNextTrack(); 
		}
		Debug.Log(scene.name);
	}

	void OnStateChange(State s)
	{
		if (s == State.GameOver)
		{
			StopCoroutine("IDecrementPitch");
			StartCoroutine("IDecrementPitch");
		}
	}

	void Update()
	{
		if (gameTrack.time >= gameTrack.clip.length)
		{
			PlayNextTrack();
		}
	}

	public void PlayNextTrack()
	{
		clipIndex++;

		if (clipIndex > clips.Count - 1)
		{
			clipIndex = 0;
		}

		Play(clips[clipIndex]);
	}



	public void Play(AudioClip clip)
	{
		StopCoroutine("ISwitchTrack"); 

		StartCoroutine("ISwitchTrack", clip); 
	}

	IEnumerator ISwitchTrack(AudioClip clip)
	{
		while(gameTrack.volume > 0)
		{
			gameTrack.volume-=Time.deltaTime * .6f; 

			yield return null; 
		}

		gameTrack.clip = clip;
		
		gameTrack.Play();
		
		StopCoroutine("IIncrementVolume");
		
		StartCoroutine("IIncrementVolume");

		gameTrack.time = Random.Range(5, gameTrack.clip.length / 4);
	}

	IEnumerator IIncrementVolume()
	{
		while (gameTrack.volume < 1)
		{
			gameTrack.volume += Time.deltaTime * .25f;
			yield return null;
		}
	}

	IEnumerator IDecrementPitch()
	{
		while (gameTrack.pitch > .6f)
		{
			gameTrack.pitch -= Time.deltaTime * .75f;
			yield return null;
		}
	}
}
