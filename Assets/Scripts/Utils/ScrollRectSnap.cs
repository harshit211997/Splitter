using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour {

	// Public Variables
	public RectTransform panel; // Holds the ScrollPanel
	public GameObject[] items;
	public RectTransform center;	// Center to compare the distance to each item
	public float d = 300;	// Distance from origin till which the items zooms in
	public float maxScale = 1.2f;	// Max zoom scale
	public int startItem = 1;
	public float speed = 10f;

	// Private Variables
	private float[] distance;	// All items' distance to the center
	private bool dragging = false;	// Will be true while we drag the panel
	private int itemDistance;	// Will hold distance between buttons
	private int minItemIndex;	// To hold index of the item, with smallest distance to center
	private bool messageSent = false;
	private bool targetNearestItem = true;

	void Start() {
		int itemLength = items.Length;
		distance = new float[itemLength];

		// Get distance between items
		itemDistance = (int)Mathf.Abs(items [0].GetComponent<RectTransform> ().anchoredPosition.x -
			items [1].GetComponent<RectTransform> ().anchoredPosition.x);

		panel.anchoredPosition = new Vector2 (-startItem * itemDistance, panel.anchoredPosition.y);
	}

	void Update() {
		for (int i = 0; i < items.Length; i++) {
			distance [i] = Mathf.Abs (center.transform.position.x - items [i].transform.position.x);
		}

		if (targetNearestItem) {
			float minDistance = Mathf.Min (distance);	// Get the min distance

			for (int i = 0; i < items.Length; i++) {
				if (minDistance == distance [i]) {
					minItemIndex = i;
				}
			}
		}

		for (int i = 0; i < items.Length; i++) {
			if (distance [i] <= d) {
				float x = distance [i];
				float y = maxScale - x / d * (maxScale - 1);
				items [i].GetComponent<RectTransform> ().localScale = new Vector3 (y, y, y);
			} else {
				items [i].GetComponent<RectTransform> ().localScale = Vector3.one;
			}
		}

		if (!dragging) {
			LerpToBttn (minItemIndex * -itemDistance);
		}
	}

	void LerpToBttn(int position) {
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * speed);

		if (Mathf.Abs (newX) >= Mathf.Abs (position) - 5f && Mathf.Abs (newX) <= Mathf.Abs (position) + 5f) {
			if (!messageSent) {
				messageSent = true;
			}
		}

		Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);

		panel.anchoredPosition = newPosition;
	}

	public void StartDrag() {
		dragging = true;
		messageSent = false;
		targetNearestItem = true;
	}

	public void EndDrag() {
		dragging = false;
	}

	public void GoToItem(int itemIndex) {
		minItemIndex = itemIndex;
		targetNearestItem = false;
	}

	public void Next() {
		if (minItemIndex + 1 < items.Length) {
			GoToItem (minItemIndex + 1);
		}
	}

	public void Prev() {
		if (minItemIndex - 1 >= 0) {
			GoToItem (minItemIndex - 1);
		}
	}

}










