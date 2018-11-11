using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SimpleButtonEvent : MonoBehaviour, IPointerClickHandler {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public virtual void OnPointerClick(PointerEventData data)
	{
		Debug.Log("Hit");
	}
}
