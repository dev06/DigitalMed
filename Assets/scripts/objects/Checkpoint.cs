using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public ParticleSystem energyDraw;

	PlayerMovement movementHandler;

	Vector3 targetPosition;

	private SpawnerHandler spawner;

	private Transform ghostContainer;

	private LineRenderer lineRenderer;





	public void Init ()
	{
		movementHandler = FindObjectOfType<PlayerMovement>();

		spawner = FindObjectOfType<SpawnerHandler>();

		transform.position = GetNextLocation();

		ghostContainer = GameObject.FindWithTag("Containers/Ghost").transform;

		lineRenderer = transform.GetComponentInChildren<LineRenderer>();

		lineRenderer.enabled = false;

		Toggle(true);
	}

	private void Toggle(bool b)
	{
		GetComponent<MeshRenderer>().enabled = b;
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine("IHover");
		}
		//transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Player") { return; }

		if (EventManager.OnCheckpointHit != null)
		{
			EventManager.OnCheckpointHit();
		}

		transform.position = GetNextLocation();
	}

	private Vector3 GetNextLocation()
	{
		Vector3 location = Vector3.up;

		Transform currentLevelObjectLocations = LevelController.Instance.CurrentLevelObject.GetChild(1);

		location = currentLevelObjectLocations.GetChild(GameplayController.Instance.checkpointsCollected).position;

		return location;
	}


	private IEnumerator IHover()
	{
		Vector3 currentPos = transform.position;

		float heightOffset = 0;

		float targetHeight = 5f;

		while (heightOffset < targetHeight)
		{
			heightOffset += Time.deltaTime * (targetHeight - heightOffset);

			if (heightOffset > targetHeight - .2f )
			{
				break;
			}

			transform.position = new Vector3(transform.position.x, heightOffset, transform.position.z);

			yield return null;
		}

		float shakeTimer = 0;

		Vector3 hoveringPos = transform.position;

		energyDraw.Play();

		while (shakeTimer < 4f)
		{
			shakeTimer += Time.deltaTime;

			transform.position = hoveringPos + (Vector3)(Random.insideUnitCircle * .1f);

			yield return new WaitForSeconds(.01f);
		}

		energyDraw.Stop();

		lineRenderer.enabled = true;

		lineRenderer.SetPosition(0, transform.position + new Vector3(0, 2f, 0));

		for (int i = 0; i < ghostContainer.childCount; i++)
		{
			lineRenderer.SetPosition(1, ghostContainer.GetChild(i).transform.position);

			ghostContainer.GetChild(i).GetComponent<Ghost>().Toggle(false);

			yield return new WaitForSeconds(.15f);
		}

		lineRenderer.enabled = false;

		Debug.Log("Out");

	}
}
