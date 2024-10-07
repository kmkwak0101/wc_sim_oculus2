﻿/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: WaitTime.cs
** --------
** This program is used to generate the graphics for the waiting screen between scenes.
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class WaitTime : MonoBehaviour
{
   public Text waitTime;
   private int sceneNumber;
   private float currentTime;
   public Text loadPercentage;
   public float timeRemaining;
   public float startTime = 30f;
   public Slider loadingSlider;
	
	
	
	/* The Start() function is called fist and it reads the current scen number and 
	** sets the max value for the UI slider.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	void Start() {
		sceneNumber = SceneManager.GetActiveScene().buildIndex;
		loadingSlider.maxValue = startTime;
		timeRemaining = startTime;
	}

	/* The Update() function is called once per frame. This function counts down from
	** a set starting time and generates graphics for the UI.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
    void Update()
    {
		
		if (timeRemaining > 0f) {
			waitTime.text =  timeRemaining.ToString();
			currentTime = startTime - timeRemaining;
			loadingSlider.value = currentTime;
			//Debug.Log(currentTime);
			loadPercentage.text = Mathf.Round((currentTime/30f)*100f).ToString() + "%";
			
			timeRemaining -= Time.deltaTime;
		}
		else {
			SceneManager.LoadScene(sceneNumber + 1);
		}
    }
}
