﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour {
	[SerializeField] private GameObject[] Lista;
	private GameObject[] pool;
	private int counter = 0;

	void Awake(){
		pool = new GameObject[Lista.Length];
		for (int i = 0; i<Lista.Length; i++){
			GameObject go = Instantiate (Lista [i]);
			go.transform.position = Vector3.one * 60;
			go.GetComponent<Product> ().Patron = this;
			pool [i] = go;
		}
	}

	public void Return(GameObject go){
		for (int i = 0; i < Lista.Length; i++){
			if(go == pool[i]){
				pool [i].transform.position = Vector3.one * 60;
				//Debug.Log (go.tag + " se recicla");
			}
		}
	}

	public GameObject Request(){
		int count = counter;
		counter++;
		if(counter>= Lista.Length){
			counter = 0;
		}
        pool[count].GetComponent<Product>().Sent();
		return pool [count];
	}

    public int TamLista() {
        return Lista.Length;
    }

    public int Count() {
        return counter;
    }
}
