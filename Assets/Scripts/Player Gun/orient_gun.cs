using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orient_gun : MonoBehaviour {
	int rotate_speed = 5;

	void Update () {
		Transform camera_transform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		Ray ray = new Ray (camera_transform.position, camera_transform.forward);
		RaycastHit hit;

		Quaternion target_rotation;

		if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
			target_rotation = Quaternion.LookRotation(hit.point - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, target_rotation, rotate_speed * Time.deltaTime);
		}
	}
}
