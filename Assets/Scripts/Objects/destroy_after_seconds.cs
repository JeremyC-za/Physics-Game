using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_after_seconds : MonoBehaviour {
	public float life_time = 5.0f;

	float time_active;

	void Start () {
		time_active = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		time_active += Time.deltaTime;
		if (time_active >= life_time) {
			Destroy (gameObject);
		}
	}
}
