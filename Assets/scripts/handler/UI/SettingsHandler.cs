using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsHandler : UserInterface {

	public Slider SFXSlider;
	public Slider MusicSlider;

	private TrackHandler trackHandler;
	private SFXController sfxController;


	public override void Init()
	{
		base.Init();
	}

	public void Start()
	{
		trackHandler = FindObjectOfType<TrackHandler>();
		Toggle(GameController.State == state);

		SFXController.VOLUME = PlayerPrefs.HasKey("SFX") ? PlayerPrefs.GetFloat("SFX") : .5f;
		TrackHandler.VOLUME = PlayerPrefs.HasKey("MUSIC") ? PlayerPrefs.GetFloat("MUSIC") : .5f;
		SFXSlider.value = SFXController.VOLUME;
		MusicSlider.value = TrackHandler.VOLUME;
		UpdateSFXVolume();
		UpdateMusicVolume();
	}

	void OnEnable ()
	{
		EventManager.OnStateChange += OnStateChange;
	}

	void OnDisable ()
	{
		EventManager.OnStateChange -= OnStateChange;
	}

	void OnStateChange(State s)
	{

		if (s != State.Settings)
		{
			Toggle(false);
			return;
		}

		Toggle(true);
	}


	public void UpdateSFXVolume()
	{
		SFXController.VOLUME = SFXSlider.value;
		SFXController.Instance.SetSFXVolume(SFXController.VOLUME);
	}

	public void UpdateMusicVolume()
	{
		TrackHandler.VOLUME = MusicSlider.value;
		trackHandler.SetMusicVolume(TrackHandler.VOLUME);
	}

	public void BackButton()
	{
		GameController.Instance.SetState(State.Menu);
		PlayerPrefs.SetFloat("SFX", SFXController.VOLUME);
		PlayerPrefs.SetFloat("MUSIC", TrackHandler.VOLUME);
	}
}
