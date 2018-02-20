using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_push_pull : MonoBehaviour {
	public float push_strength = 1.0f;
	public float pull_strength = 1.0f;
	public float range = 200.0f;

	float pull_charge = 0.0f;
	float push_charge = 0.0f;
	GameObject player;

	const float CHARGE_LIMIT = 5.0f;
	const int FORCE_MULTIPLIER = 200;
	const int PUSH = 1;
	const int PULL = -1;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		if (Input.GetButton ("Push")) {
			push_charge += Time.deltaTime;
		}

		else if (Input.GetButtonUp ("Push")) {
			Push ();
		}

		if (Input.GetButton ("Pull")) {
			pull_charge += Time.deltaTime;
		}

		else if (Input.GetButtonUp ("Pull")) {
			Pull ();
		}
	}

	void Push() {
		push_charge = push_charge > CHARGE_LIMIT ? CHARGE_LIMIT : push_charge;
		PerformRayCast (PUSH, push_charge, push_strength);
		// PerformSphereCast (PUSH, push_charge, push_strength);
		push_charge = 0.0f;
	}

	void Pull(){
		pull_charge = pull_charge > CHARGE_LIMIT ? CHARGE_LIMIT : pull_charge;
		PerformRayCast (PULL, pull_charge, pull_strength);
		// PerformSphereCast (PULL, pull_charge, pull_strength);
		pull_charge = 0.0f;
	}

	// Trying spherecast. It's doing weird shit though...
	void PerformSphereCast(int direction, float charge, float strength) {
		Transform camera_transform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		RaycastHit[] hits = Physics.SphereCastAll (camera_transform.position, 0.3f, camera_transform.forward, range);

		for (int i = 0; i < hits.Length; i++) {
			RaycastHit sphere_hit = hits [i];
			// Ray ray = new Ray (camera_transform.position, sphere_hit.point);
			RaycastHit ray_hit;

			Debug.DrawRay (camera_transform.position, sphere_hit.transform.position, Color.red);
			if (Physics.Raycast (camera_transform.position, sphere_hit.transform.position, out ray_hit, range)) {
				Debug.Log ("Raycast hit: " + ray_hit.collider.gameObject.name);
				if (ray_hit.collider == sphere_hit.collider) {
					if (ray_hit.rigidbody != null) {
						Vector3 force = FORCE_MULTIPLIER * strength * charge * direction * camera_transform.forward;
						ray_hit.rigidbody.AddForceAtPosition (force, ray_hit.point, ForceMode.Impulse);
					}
				}
			}
		}
	}

	void PerformRayCast(int direction, float charge, float strength){
		Transform camera_transform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		Ray ray = new Ray (camera_transform.position, camera_transform.forward);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
			if (hit.rigidbody != null) {
				Vector3 force = FORCE_MULTIPLIER * strength * charge * direction * camera_transform.forward;
				hit.rigidbody.AddForceAtPosition (force, hit.point, ForceMode.Impulse);
			} else {
				if (direction == PUSH) {
					Vector3 force = FORCE_MULTIPLIER * strength * charge * direction * -0.7f * camera_transform.forward;
					player.GetComponent<Rigidbody>().AddForce (force, ForceMode.Impulse);
				}
			}
		}
	}
}