using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_move : MonoBehaviour {
	public Vector3 direction = new Vector3(1, 1, 1);
	public float speed_multilpier = 1.0f;

	void Update () {
		transform.position = (transform.position + (direction * speed_multilpier));
	}
}
