using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityStandardAssets.ImageEffects; 
public class FinalHandler : MonoBehaviour {

	public CanvasGroup creditsCanvasGroup; 

	public Animation creditsAnimation; 

	public void RollCredits()
	{
		creditsCanvasGroup.alpha = 1f;
		creditsCanvasGroup.blocksRaycasts = true; 
		creditsAnimation.Play(); 
		GetComponent<Animation>().Stop();
		GetComponent<ScreenOverlay>().intensity = 1f; 
	}
}
