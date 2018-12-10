﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtom : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(GameManager.Instance.IsPlaying){
				if(!GameManager.Instance.IsDeath)
					MenuManager.Instance.PauseGame();
			}
			else
				Application.Quit();
		}
	}
}
