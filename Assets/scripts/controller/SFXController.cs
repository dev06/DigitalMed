using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppResources
{
	public static AudioClip thunder_1 = (AudioClip)Resources.Load("audio/sfx/thunder_1") as AudioClip;
	public static AudioClip thunder_2 = (AudioClip)Resources.Load("audio/sfx/thunder_2") as AudioClip;

	public static AudioClip swish_1 = (AudioClip)Resources.Load("audio/sfx/idol_swish_1") as AudioClip;
	public static AudioClip thunder_zap = (AudioClip)Resources.Load("audio/sfx/thunder_zap") as AudioClip;

}

public class SFXController : MonoBehaviour {


	public static SFXController Instance;

	public AudioSource thunder, sfxOpenGate;

	public List<AudioSource> sfxSources = new List<AudioSource>();

	private float defaultSFX0, defaultSFX1, defaultSFX2, defaultSFX3, defaultSFX4, defaultSFX5;

	public static float VOLUME = 1F;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		defaultSFX0 = sfxSources[0].volume;
		defaultSFX1 = sfxSources[1].volume;
		defaultSFX2 = sfxSources[2].volume;
		defaultSFX3 = sfxSources[3].volume;
		defaultSFX4 = sfxSources[4].volume;
		defaultSFX5 = sfxSources[5].volume;
	}


	public void SetSFXVolume(float volume)
	{
		sfxSources[0].volume = defaultSFX0 * volume;
		sfxSources[1].volume = defaultSFX1 * volume;
		sfxSources[2].volume = defaultSFX2 * volume;
		sfxSources[3].volume = defaultSFX3 * volume;
		sfxSources[4].volume = defaultSFX4 * volume;
		sfxSources[5].volume = defaultSFX5 * volume;

		// for (int i = 0; i < sfxSources.Count; i++)
		// {
		// 	sfxSources[i].volume = volume;
		// }
	}


	void Start ()
	{
		//Debug.Log(AppResources.thunder_1);
	}


	void Update ()
	{

	}

	public void PlayThunder()
	{
		AudioClip clipToPlay = Random.Range(0, 2) == 0 ? AppResources.thunder_1 : AppResources.thunder_2;
		thunder.clip = clipToPlay;
		thunder.Play();
	}

	public void SFXOpenGate()
	{
		sfxOpenGate.Play();
	}
}
