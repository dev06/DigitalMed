using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour {

	public void ToMenu()
	{
		PlayerPrefs.DeleteAll(); 
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
