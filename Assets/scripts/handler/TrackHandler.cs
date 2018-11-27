using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHandler : MonoBehaviour {

	public List<AudioClip> clips = new List<AudioClip>();

	public AudioSource gameTrack;

	private int clipIndex;

	void OnEnable()
	{
		EventManager.OnStateChange += OnStateChange;
	}

	void OnDisable()
	{
		EventManager.OnStateChange -= OnStateChange;
	}
	void Start () {
		Play(clips[clipIndex]);
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
			clipIndex++;

			if (clipIndex > clips.Count - 1)
			{
				clipIndex = 0;
			}

			Play(clips[clipIndex]);
		}
	}



	public void Play(AudioClip clip)
	{
		gameTrack.volume = 0;
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
