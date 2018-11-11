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


	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}


	void Start ()
	{
		//Debug.Log(AppResources.thunder_1);
	}


	void Update () {

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
