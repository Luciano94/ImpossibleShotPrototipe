﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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

    [SerializeField] GameObject spawnPattern;
    [SerializeField] SpawnEnv spawnEnv;
    [SerializeField] float terrainSpeed = 50f;
    [SerializeField] float fOVPerLevel = 5f;
    [SerializeField] float maxFOV = 80f;
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float speedPerLevel = 10f;
    [SerializeField] int cantOfEnemiesPerLevel = 5;
    [SerializeField] int pointsperEne = 100;
    [SerializeField] AudioSource deadShot;
    [SerializeField] AudioSource enemyShot;
    private int points = 0;
    int level = 1;
    private int cantOfEnemies;
    private int cantEnemies=0;

    public float TerrainSpeed {
        get { return terrainSpeed; }
    }

    public void Death() {
        deadShot.Play();
        terrainSpeed = 0;
        Invoke("terminate", 0.5f);
    }

    private void terminate(){
        SceneManager.LoadScene(0);
    }

    public void EnemyDeath(){
        enemyShot.Play();
        cantOfEnemies ++;
        points += pointsperEne;
        MenuManager.Instance.UpdateEnemies(cantOfEnemies,cantOfEnemiesPerLevel);
        MenuManager.Instance.UpdatePoints(points);
        EnemiesControl();
    }

    private void EnemiesControl(){
        if(cantOfEnemies >= cantOfEnemiesPerLevel){
            level++;
            if(level % 2 == 0)
                LvlPar();
            else 
                LvlImpar();
            cantEnemies ++;
            cantOfEnemiesPerLevel += cantEnemies;
            cantOfEnemies = 0;
            UpdateHUD();
        }
    }

    private void LvlPar(){
        SpeedControl();
        FovControl();
        spawnPattern.GetComponent<SpawnPattern>().TimePerObs();
    }

    private void LvlImpar(){
        spawnPattern.GetComponent<SpawnPattern>().TimePerObs();
        spawnPattern.GetComponent<SpawnPattern>().RandomizePattern();
    }

    private void UpdateHUD(){
        MenuManager.Instance.UpdateEnemies(cantOfEnemies,cantOfEnemiesPerLevel);
        MenuManager.Instance.UpdateLvl(level);
    }

    private void SpeedControl(){
        if(terrainSpeed + speedPerLevel <= maxSpeed){
            terrainSpeed += speedPerLevel;
            spawnEnv.UpdateTime();
        }
    }
    private void  FovControl(){
        if(Camera.main.fieldOfView + fOVPerLevel <= maxFOV){
            float nextFOV = Camera.main.fieldOfView + fOVPerLevel;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, nextFOV, 1f * Time.deltaTime);
        }
    }

    private void Awake() {
        cantOfEnemies = 0;
        MenuManager.Instance.UpdatePoints(points);
        MenuManager.Instance.UpdateEnemies(cantOfEnemies,cantOfEnemiesPerLevel);
        MenuManager.Instance.UpdateLvl(level);
    }
}
