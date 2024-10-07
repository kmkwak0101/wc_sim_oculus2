﻿/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: CSVReporter.cs
** --------
** This program is used to read player position data and send it to the CSVmanager.cs file to save the 
** position data to a .csv file.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CSVReporter : MonoBehaviour
{
	public Rigidbody  rb;
	private string[] vehiclePosition = new string[5];//Bing
	private Vector3 newPosition;
	private Vector3 CenterPoint = Vector3.zero;
	private float nextActionTime = 0.0f;
	public float period = 0.5f;
	public string username;
	private string sceneName;
	private string CurrentTime;
	private string CurrentDate;
	private int coneHit = 0;
	
	
	/* The OnEnable() funcion is executed as soon as the overall script is called. It retrieves the name 
	** given in the main menu by the user and saves it to a local varaible.
	** Parameters:
	**    N/A
	** Return:
	**    null
	*/
	void OnEnable(){
		username = PlayerPrefs.GetString("Name");
	}
	
	
	
	/* The Start() funciton is called first after the OnEnable() in the progam and is used to retrieves
	** the name of the level and create a new file path using the user's name and level. It also creates
	** an addtional folder in the saved_games folder that is the current date.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
    void Start()
    {
		sceneName = SceneManager.GetActiveScene().name;
		CurrentTime = System.DateTime.Now.ToString("MM_dd_yyyy___HH_mm");
		CurrentDate = System.DateTime.Now.ToString("MM_dd_yyyy");
		CSVmanager.reportDirectoryName = CSVmanager.reportDirectoryName + "/" + CurrentDate;
		CSVmanager.reportFileName = username + "_" + sceneName + "_" + CurrentTime + ".csv"; 
		NewReport();
    }

	/* The Update() funciton is called every frame and checks if the current time is a wait period past the last
	** recorded time. If this is true then the GetPlayerData() function is called and a new time is recorded.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
    void Update()
    {
		if (Time.time > nextActionTime ) {
			nextActionTime = Time.time + period;
			GetPlayerData();
		}
		
    }
	

	/* The GetPlayerData() function reads the current position of the rigidBody in the game relative to the center point (0,0,0)
	** and sends this new vector to the CSVmanager to save to the .csv file.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	void GetPlayerData() {
		Vector3 newPosition = rb.position - CenterPoint;
		//Debug.Log(String.Format("Player Position is {0}, newPosition is {1}", rb.position, newPosition));
		vehiclePosition[0] = newPosition.x.ToString();
		vehiclePosition[1] = newPosition.y.ToString();
		vehiclePosition[2] = newPosition.z.ToString();
		vehiclePosition[3] = System.DateTime.Now.ToString("HH:mm:ss");//Bing
		vehiclePosition[4] = coneHit.ToString();//Bing
		AppendToReport(vehiclePosition);
	}
	
	
	/* The AppendToReport() function takes the incoming data and passes it to CSVmanager to be saved to the .csv file.
	** Parameters:
	**    string[] dataToAppend - Vector3 data
	** Return:
	**    N/A
	*/
	void AppendToReport(string[] dataToAppend) {
		CSVmanager.AppendToReport(dataToAppend);
	}
	
	
	/* The NewReport() funcion creates a new .csv file if one does not already exist.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	void NewReport() {
		CSVmanager.CreateReport();
	}
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "cone") {
			coneHit = coneHit + 1;
			Debug.Log("Hit");
		}
	}
}

