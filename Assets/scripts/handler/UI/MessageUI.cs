using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageUI : UserInterface {

	public CanvasGroup messageGroup;
	public Text scriptText;
	public string scriptMessage_1;
	public string scriptMessage_2;
	public string scriptMessage_3;

	public override void Init()
	{
		base.Init();
	}


	public void Start()
	{
		Toggle(GameController.State == state);
	}

	void OnEnable () {
		EventManager.OnScrollPostHit += OnScrollPostHit;
		EventManager.OnStateChange += OnStateChange;
	}


	void OnDisable () {
		EventManager.OnScrollPostHit -= OnScrollPostHit;
		EventManager.OnStateChange -= OnStateChange;
	}

	void OnScrollPostHit()
	{
		GameController.Instance.SetState(state);

		FindObjectOfType<PlayerMovement>().LockMove = true;
		StopCoroutine("IWriteScript");
		StartCoroutine("IWriteScript");
	}

	public void CloseMessage()
	{
		Toggle(false);

		scriptText.text = "";
		FindObjectOfType<PlayerMovement>().LockMove = false;
		StopCoroutine("IWriteScript");

		FindObjectOfType<Checkpoint>().SetTargetLocation();

		GameController.Instance.SetState(State.Game);
	}

	void OnStateChange(State s)
	{
		if (s != state)
		{
			Toggle(false);
			return;
		}

		Toggle(true);
	}

	IEnumerator IWriteScript()
	{
		string msg = "";
		for (int i = 0; i < scriptMessage_1.Length; i++)
		{
			msg += scriptMessage_1[i];
			scriptText.text = msg;
			yield return new WaitForSeconds(.05f);
		}
		msg += "\n";
		for (int i = 0; i < scriptMessage_2.Length; i++)
		{
			msg += scriptMessage_2[i];
			scriptText.text = msg;
			yield return new WaitForSeconds(.05f);
		}
		msg += "\n";
		for (int i = 0; i < scriptMessage_3.Length; i++)
		{
			msg += scriptMessage_3[i];
			scriptText.text = msg;
			yield return new WaitForSeconds(.1f);
		}
	}
}
