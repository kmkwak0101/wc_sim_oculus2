﻿/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: NewCSVReader.cs
** --------
** This program is used to read .csv files off of the local PC from the custom folder created in the
** CSVmanager.cs script. using the player data read from the .csv file a line a created in the current
** scene that is the path the player took when running the track.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;


public class CSVReader : MonoBehaviour
{
	public Text displayConeHit;
	private string file_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
	private string[] splitData;
	private Vector3 oldPosition = Vector3.zero;
	private Vector3 newPosition;
	private int lastLine;
	private float conesHit;
	
	
	/* The Start() funciton is called first and is used to read the .csv file based on the folder and file name
	** given by the CSVmanager.cs script. This data is then split into individual x, y, and z points and saved to 
	** a Vector3 (newPosition). The DrawLine() function is called with the oldPosition and newPosition vectors.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
    void Start()
    {
		file_path = file_path + "/" + CSVmanager.reportDirectoryName + "/" + CSVmanager.reportFileName;
		string fileData = File.ReadAllText(file_path);
		splitData = fileData.Split(new char[] {'\n'});
		Color TeamColor = new Color(95f,168f,220f);
		lastLine = splitData.Length -1;
		//Debug.Log(lastLine);
		
		
		for (int i = 1; i < splitData.Length - 1; i++) {
			string[] row = splitData[i].Split(new char[] {','});
			newPosition.x = float.Parse(row[0]); 
			newPosition.y = float.Parse(row[1]); 
			newPosition.z = float.Parse(row[2]);
			if (i == lastLine-1) {
				conesHit = float.Parse(row[4]);
				//Debug.Log(conesHit);
				displayConeHit.text = "Cones Hit " + conesHit.ToString() + " times";
			}
			DrawLine(oldPosition,newPosition,TeamColor);
			oldPosition = newPosition;
		}		
      //ScreenCap.TakeScreenshot();
		ScreenShotHandler.TakeScreenshot_Static(1920, 1080);
    }


	/* The DrawLine() function takes two vectors and draws a line between them. The color and width of the line are 
	** also decided in this function.
	** Parameters:
	**    Vector3 start - the starting vector for the line
	** 	  Vector3 end   - the ending vector for the line 
	**	  Color color   - the color that the line will between  
	** Return:
	**    N/A
	*/	
	void DrawLine(Vector3 start, Vector3 end, Color color) {
             GameObject myLine = new GameObject();
             myLine.transform.position = start;
             myLine.AddComponent<LineRenderer>();
             LineRenderer lr = myLine.GetComponent<LineRenderer>();
            //lr.material = new Material(lineShader);
             lr.startColor = color;
             lr.startWidth = 0.1f;
             lr.SetPosition(0, start);
             lr.SetPosition(1, end);
             //GameObject.Destroy(myLine, duration);
    }
}
