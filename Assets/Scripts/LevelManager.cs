﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public bool called;
	public Transform horn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Called obnly once
	}

	public IEnumerator HornCall()
	{
		called = true;
		yield return null;
		called = false;
	}
}
