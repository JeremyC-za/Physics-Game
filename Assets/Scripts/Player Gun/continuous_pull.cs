using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continuous_pull : MonoBehaviour {
	public float continuous_pull_strength;
	GameObject player;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate () {
		if (Input.GetButton ("Pull")) {
			Transform camera_transform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
			Ray ray = new Ray (camera_transform.position, camera_transform.forward);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				if (hit.rigidbody == null) {
					Vector3 force = continuous_pull_strength * camera_transform.forward;
					player.GetComponent<Rigidbody> ().AddForce (force);
				}
			}
		}
	}
}
