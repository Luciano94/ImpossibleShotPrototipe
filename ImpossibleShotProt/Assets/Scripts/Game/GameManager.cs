﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance {
        get {
            instance = FindObjectOfType<GameManager>();
            if(instance == null) {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField] float multPerEnemy;
    [SerializeField] GameObject spawnPattern;
    [SerializeField] SpawnEnv spawnEnv;
    [SerializeField] float terrainSpeed = 80f;
    [SerializeField] AudioSource deadShot;
    [SerializeField] AudioSource enemyShot;
    [SerializeField] BulletMovement playerMov;
    [SerializeField] BulletSpin playerSpin;
    [SerializeField] ParticleSystem blood;
    [SerializeField] ParticleSystem trail;
    private bool tutorialMode = false;
    private float multiplicador;
    private float points = 0;
    int level = 1;
    private PatternSpawner spawn;

    public int Level{
        get{return level;}
    }

    public float TerrainSpeed {
        get { return terrainSpeed; }
    }

    public void PlayTutorial(){
        tutorialMode = true;
    }

    public bool TutorialMode{
        get{return tutorialMode;}
    }

    public void EndTutorial(){
        tutorialMode = false;
        multiplicador = 1;
        points = 0;
		MenuManager.Instance.UpdatePoints(points, 0, multiplicador);
    }
    public void Death() {
        deadShot.Play();
        playerMov.enabled = false;
        playerSpin.enabled = false;
        /*trail.Pause();
        terrainSpeed = 0;*/
        Handheld.Vibrate();
        terminate();
        Time.timeScale = 0.0f;
    }

    public void Revive(){
        Time.timeScale = 1.0f;
        MenuManager.Instance.ContinueGame();
    }

    private void terminate(){
        MenuManager.Instance.FinishGame();
    }

    public void EnemyDeath(int value){
        enemyShot.Play();
        blood.Play();
        spawn.UpdateStage();
        multiplicador += multPerEnemy;
        multiplicador = (float)Math.Round(multiplicador, 2);
        var AddedScore = value * multiplicador;
        points += AddedScore;
		MenuManager.Instance.UpdatePoints(points, AddedScore, multiplicador);
    }

    private void Awake(){
        if (!PlayerPrefs.HasKey("Tutorial"))
            PlayerPrefs.SetInt("Tutorial", 1);
		spawn = spawnPattern.GetComponent<PatternSpawner>();
        multiplicador = 1;
		MenuManager.Instance.UpdatePoints(points, 0, multiplicador);
    }

    public void StartGame(){
        FirstPlay.Instance.play();
        if(!tutorialMode){
            spawn.Begin();
        }
    }

    public void TutorialSpawnBegin(){
        if(tutorialMode){
            spawn.Begin();
        }
    }

    public bool InTutorial(){
        return tutorialMode;
    }
}
