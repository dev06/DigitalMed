using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerbeamFlash : MonoBehaviour {

	private Image image;

	void OnEnable()
	{
		EventManager.OnLevelComplete += OnLevelComplete;
	}

	void OnDisable()
	{
		EventManager.OnLevelComplete -= OnLevelComplete;
	}

	void Start ()
	{
		image = GetComponent<Image>();
	}

	// Update is called once per frame
	public void IncreaseFade () {
		StopCoroutine("IIncreaseFade");
		StartCoroutine("IIncreaseFade");
	}

	private void OnLevelComplete()
	{
		StopCoroutine("IIDecreaseFade");
		StartCoroutine("IDecreaseFade");
	}

	IEnumerator IIncreaseFade()
	{
		float alpha = 0;

		while (alpha < 1)
		{
			float noise = Random.Range(-.02f, .02f);

			alpha += Time.deltaTime + noise;

			image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

			yield return new WaitForSeconds(Time.deltaTime * 7f);
		}

		image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

		yield return new WaitForSeconds(.5f);

		if (EventManager.OnLevelComplete != null)
		{
			EventManager.OnLevelComplete();
		}

	}

	IEnumerator IDecreaseFade()
	{
		float alpha = image.color.a;

		while (alpha > 0)
		{

			alpha -= Time.deltaTime;

			image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

			yield return new WaitForSeconds(Time.deltaTime * .7f);
		}
	}
}
