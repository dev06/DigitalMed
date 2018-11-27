using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextWrite : MonoBehaviour {

	public string message;
	public Text text;

	void Start ()
	{
		text.text = "";
	}

	void OnEnable()
	{
		EventManager.OnStateChange += OnStateChange;
	}
	void OnDisable()
	{
		EventManager.OnStateChange -= OnStateChange;
	}

	void OnStateChange(State s)
	{
		if (s == State.GameOver)
		{
			StartCoroutine("IWriteScript");
		}
	}

	IEnumerator IWriteScript()
	{
		while (true)
		{
			string msg = "";
			for (int i = 0; i < message.Length; i++)
			{
				msg += message[i];
				text.text = msg;
				yield return new WaitForSeconds(.25f);
			}
			yield return new WaitForSeconds(3f);
		}
	}
}
