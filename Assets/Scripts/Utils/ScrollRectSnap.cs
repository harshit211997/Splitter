using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour {

	// Public Variables
	public RectTransform panel; // Holds the ScrollPanel
	public List<GameObject> items;
	public RectTransform center;	// Center to compare the distance to each item
	public float d = 300;	// Distance from origin till which the items zooms in
	public float maxScale = 1.2f;	// Max zoom scale
	public int startItem = 1;
	public float speed = 10f;
	public GameObject templateRocket;
	public RocketsList listContainer;

	// Private Variables
	private Image left, right;
	private Text title, description;
	private Button select;
	private float[] distance;	// All items' distance to the center
	private bool dragging = false;	// Will be true while we drag the panel
	private int itemDistance;	// Will hold distance between buttons
	private int minItemIndex;	// To hold index of the item, with smallest distance to center
	private bool messageSent = false;
	private bool targetNearestItem = true;
	private Transform container;

	void Start() {
		//Populate the Scroll Panel
		container = GetComponent<ScrollRect> ().content.transform;

		Vector2 pos = new Vector2 (0, 21);
		for (int i = 0; i < listContainer.rocketsList.Count; i++) {
			GameObject rocket = Instantiate (templateRocket);
			rocket.transform.parent = container;
			rocket.GetComponent<RectTransform> ().localScale = Vector3.one;
			gameObject.GetComponentInParent<ScrollRectSnap> ().items.Add (rocket);

			title = rocket.transform.Find ("Rocket Title").GetComponent<Text>();
			Transform rocketSprite = rocket.transform.Find ("Rocket Sprite").transform;
			left = rocketSprite.Find ("Rocket Sprite left").GetComponent<Image> ();
			right = rocketSprite.Find ("Rocket Sprite right").GetComponent<Image>();
			description = rocket.transform.Find ("Rocket Description").GetComponent<Text>();

			rocket.GetComponent<RectTransform> ().anchoredPosition = pos;
			pos.x += 720;
		}

		int itemLength = items.Count;
		print (itemLength);
		distance = new float[itemLength];

		// Get distance between items
		itemDistance = (int)Mathf.Abs(items [0].GetComponent<RectTransform> ().anchoredPosition.x -
			items [1].GetComponent<RectTransform> ().anchoredPosition.x);

		panel.anchoredPosition = new Vector2 (-startItem * itemDistance, panel.anchoredPosition.y);
	}

	void Update() {
		for (int i = 0; i < items.Count; i++) {
			distance [i] = Mathf.Abs (center.transform.position.x - items [i].transform.position.x);
		}

		if (targetNearestItem) {
			float minDistance = Mathf.Min (distance);	// Get the min distance

			for (int i = 0; i < items.Count; i++) {
				if (minDistance == distance [i]) {
					minItemIndex = i;
				}
			}
		}

		for (int i = 0; i < items.Count; i++) {
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
		if (minItemIndex + 1 < items.Count) {
			GoToItem (minItemIndex + 1);
		}
	}

	public void Prev() {
		if (minItemIndex - 1 >= 0) {
			GoToItem (minItemIndex - 1);
		}
	}

}










