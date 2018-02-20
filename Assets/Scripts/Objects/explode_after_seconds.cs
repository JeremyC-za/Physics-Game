using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode_after_seconds : MonoBehaviour {
	public GameObject explosion;
	public float explode_after = 2.0f;
	public float explosion_radius = 5.0f;
	public float explosion_power = 300.0f;
	public float explosion_lift = 0.5f;

	float time_active;

	void Start () {
		time_active = 0.0f;
	}

	void Update () {
		time_active += Time.deltaTime;
		if (time_active >= explode_after) {
			Vector3 explosion_position = transform.position;
			Collider[] colliders = Physics.OverlapSphere (explosion_position, explosion_radius);

			foreach (Collider hit in colliders) {
				Rigidbody hit_rb = hit.GetComponent<Rigidbody>();
				if (hit_rb != null) {
					hit_rb.AddExplosionForce(explosion_power, explosion_position, explosion_radius, explosion_lift, ForceMode.Impulse);
				}
			}
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
