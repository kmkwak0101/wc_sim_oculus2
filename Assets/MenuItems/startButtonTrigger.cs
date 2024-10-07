/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: startButtonTrigger.cs
** --------
** This program checks to see if the start button on the menu screen has been pressed, if it has then the next scene is loaded.
*/





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startButtonTrigger : MonoBehaviour
{
 //Make sure to attach these Buttons in the Inspector
    public Button startButton;
	private int sceneNumber;

    void Start()
    {
		sceneNumber = SceneManager.GetActiveScene().buildIndex;
		//Debug.Log(sceneNumber);
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        startButton.onClick.AddListener(startNextScene);
       
    }

    void startNextScene()
    {
        SceneManager.LoadScene(sceneNumber + 1);
		
    }
}
