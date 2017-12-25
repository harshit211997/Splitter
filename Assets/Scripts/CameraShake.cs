using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public float maxAngle, maxOffset;

	private Transform cameraTrans;
	private float trauma = 0;
	private Vector3 originalAngles, originalPos;
	private Vector3 shakeAngle, shakePos;
	private Vector3 position;

	void Start () {
		cameraTrans = GetComponent<Transform> ();
		originalAngles = cameraTrans.eulerAngles;
		originalPos = cameraTrans.position;
		shakeAngle = Vector3.zero;
		shakePos = Vector3.zero;
	}
	
	void Update () {

		if (trauma < 0) {
			trauma = 0;
		} else {
			trauma -= Time.deltaTime;
		}

		float shake = Mathf.Pow (trauma, 3);
		float angle = maxAngle * shake * Random.Range (-1.0f, 1.0f);
		shakeAngle.z = angle;

		float offsetX = maxOffset * shake * Random.Range (-1.0f, 1.0f);
		float offsetY = maxOffset * shake * Random.Range (-1.0f, 1.0f);
		shakePos.x = offsetX;
		shakePos.y = offsetY;

		cameraTrans.eulerAngles = originalAngles + shakeAngle;
		cameraTrans.position = originalPos + shakePos;

	}

	public void Activate() {
		trauma = 1;
	}
}
