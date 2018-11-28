using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffects : MonoBehaviour
{

	private int index = 0;

	public void TriggerParticle(Vector3 position)
	{
		Transform t = transform.GetChild(index);
		t.position = position;
		t.GetComponent<ParticleSystem>().Play();
		index++;

		if (index > transform.childCount - 1)
		{
			index = 0;
		}
	}
}
