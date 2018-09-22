﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	private static MenuManager instance;

	public static MenuManager Instance{
		get{
			instance = FindObjectOfType<MenuManager>();
			if(instance == null){
				GameObject go = new GameObject("MenuManager");
				instance = go.AddComponent<MenuManager>();
			}
			return instance;
		}
	}

	[SerializeField] private GameObject principal;
	[SerializeField] private GameObject inGame;
	[SerializeField] private GameObject pause;
	[SerializeField] private Text pointsTxt;
	[SerializeField] private Text levelTxt;
	[SerializeField] private Text eneTxt;

	private void Awake() {
		principal.SetActive(true);
		inGame.SetActive(false);
		pause.SetActive(false);
		Time.timeScale = 0f;
	} 

	public void PauseGame(){
		inGame.SetActive(false);
		pause.SetActive(true);
		Time.timeScale = 0f;
	}

	public void Resume(){
		pause.SetActive(false);
		inGame.SetActive(true);
		Time.timeScale = 1f;
	}

	public void StartGame(){
		Time.timeScale = 1f;
		principal.SetActive(false);
		inGame.SetActive(true);
	}

	public void UpdatePoints(int value){
		pointsTxt.text = "Points: " + value.ToString();
	}

	public void UpdateLvl(int value){
		levelTxt.text = "Level: " + value.ToString();
	}

	public void UpdateEnemies(int actEne, int totalEne){
		eneTxt.text = actEne.ToString() + " / " + totalEne.ToString();
	}

	private void Update() {
		if(Input.GetButton("Submit")){
			PauseGame();
		}
	}
}
