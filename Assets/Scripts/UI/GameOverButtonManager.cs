using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverButtonManager : MonoBehaviour {

	public Animator fadeOut;
	public AudioClip click;
	public AudioSource source;
	public GameObject settingsPanel;
	public GameObject scorePanel;
	public GameObject rocketsPanel;

	public void OpenSettings() {
		settingsPanel.SetActive (true);
		scorePanel.SetActive (false);
	}

	public void OnClickBack() {
		settingsPanel.SetActive (false);
		scorePanel.SetActive (true);
		rocketsPanel.SetActive (false);
	}

	public void OnClickHome() {
		fadeOut.SetTrigger ("Home");
		if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
			source.PlayOneShot (click);
		}
	}

	public void OnClickPlay() {
		fadeOut.SetTrigger ("GameOver");
		if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
			source.PlayOneShot (click);
		}
	}

	public void OnClickRocket() {
		rocketsPanel.SetActive (true);
		scorePanel.SetActive (false);
	}
}
