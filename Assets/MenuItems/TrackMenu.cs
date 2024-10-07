using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrackMenu : MonoBehaviour
{
	public Button startButton;
	private int sceneSelection = 5;
	
	
	void Start()
    {
		//Debug.Log(sceneNumber);
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        startButton.onClick.AddListener(sceneSelect);
		Debug.Log("Starting Selection");
       
    }
	
	public void handleInputSelection(int val) {
		Debug.Log("Choosing");
		
		if (val == 0) {
			Debug.Log("Track 1");
			sceneSelection = 5;
			Debug.Log("Scene 5");
		}
		if (val == 1) {
			Debug.Log("Track 2");
			sceneSelection = 8;
			Debug.Log("Scene 8");
		}
		if (val == 2) {
			Debug.Log("Track 3");
			sceneSelection = 11;
			Debug.Log("Scene 11");
		}
	}
	
	private void sceneSelect() {
		SceneManager.LoadScene(sceneSelection);
	}
	
	
}
