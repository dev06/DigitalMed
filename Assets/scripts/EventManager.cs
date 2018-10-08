﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public delegate void Gameplay();
	public static Gameplay OnCheckpointHit;
	public static Gameplay OnGameOver;
	public static Gameplay OnLevelComplete;

}
