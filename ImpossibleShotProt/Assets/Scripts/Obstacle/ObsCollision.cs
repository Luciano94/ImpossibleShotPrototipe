﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsCollision : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		GameManager.Instance.Death();
		Debug.Log("Colision");
	}
}
