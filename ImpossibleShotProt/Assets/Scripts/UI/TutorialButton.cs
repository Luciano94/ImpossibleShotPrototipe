﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour{

	public void PlayTutorial(){
		TutorialManager.Instance.TutorialSelected();
	}
}