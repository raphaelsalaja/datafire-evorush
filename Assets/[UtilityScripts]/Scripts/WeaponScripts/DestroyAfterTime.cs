using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
	public float secondsToLive = 5.0f;
	public bool startManually = false;

	private float startTime;
	private bool isStarted = false;
	// Use this for initialization
	void Awake () {
		startTime = Time.time;
	}

	public void StartTimer() {
		startTime = Time.time;
		isStarted = true;
	}

	// Update is called once per frame
	void Update () {
		if (startManually && !isStarted) {
			return;
		}

		if (Time.time - startTime > secondsToLive) {
			Destroy (gameObject);
		}
	}
}
