﻿using UnityEngine;

public class EnvScript : MonoBehaviour {
	[SerializeField] float distZ;
	private float speed;

	private void LateUpdate() {
		speed = GameManager.Instance.TerrainSpeed;
		transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
		if(transform.position.z < distZ)
			SpawnEnv.Instance.Despawn(gameObject);
	}
}
