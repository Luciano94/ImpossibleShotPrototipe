﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTutorialButtom : MonoBehaviour {

	public void NoPlayTutorial(){
        PlayerPrefs.SetInt("Tutorial", 0);
        PlayerPrefs.Save();
	}
}