using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_grenade : MonoBehaviour {
	public GameObject grenade;
	public GameObject launch_point;
	public GameObject origin_point;
	public float throw_multiplier = 1.0f;

	const int FORCE_MULTIPLIER = 5000;
	const int PLAYER_SPEED_MULTIPLIER = 500;
	// update method 
	void Update () {
		// check if our grenade key is being pressed down
		// this will keep throwing grenades as long as the user is holding the key down
		if (Input.GetKeyDown ("g")) {
			GameObject spawned_grenade = Instantiate(grenade, launch_point.transform.position, Quaternion.identity);
			Rigidbody grenade_rb = spawned_grenade.GetComponent<Rigidbody>();
			Vector3 direction = (launch_point.transform.position - origin_point.transform.position).normalized;

			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Rigidbody player_rb = player.GetComponent<Rigidbody>();
			float player_speed = player_rb.velocity.magnitude;

			Vector3 force = (direction * throw_multiplier) * (FORCE_MULTIPLIER + (player_speed * PLAYER_SPEED_MULTIPLIER));
			grenade_rb.AddForce (force, ForceMode.Force);
		}
	}
}
