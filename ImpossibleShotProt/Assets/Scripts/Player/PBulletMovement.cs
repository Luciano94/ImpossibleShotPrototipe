﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBulletMovement : MonoBehaviour {


	[SerializeField] float Speed;

	private Rigidbody rb;
	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		Vector3 direc = Vector3.zero;
		direc += Vector3.up * Input.GetAxis ("Vertical") * Speed * Time.deltaTime;
		direc += Vector3.right * Input.GetAxis ("Horizontal") * Speed * Time.deltaTime;
		rb.AddForce (direc);
		Rotate();
	}

	private void Rotate(){
		if (Input.GetAxis("Vertical") == 0)
			transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		else
			transform.rotation = new Quaternion(-(0.1f * Input.GetAxis("Vertical")),transform.rotation.y, transform.rotation.z, transform.rotation.w);
		if (Input.GetAxis("Horizontal") == 0)
			transform.rotation = new Quaternion(transform.rotation.x,0, transform.rotation.z, transform.rotation.w);
		else
			transform.rotation = new Quaternion(transform.rotation.x, (0.1f * Input.GetAxis("Horizontal")), transform.rotation.z, transform.rotation.w);
	}
}