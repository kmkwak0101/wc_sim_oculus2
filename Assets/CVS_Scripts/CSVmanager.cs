﻿/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: CSVmanger.cs
** --------
** This program is create new directories, create new .csv files, check for existing files, and
** append to existing .csv files.
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class CSVmanager
{
	
	
	/*The reportFileName is edited by the CSVReporter.cs script depending on what the user's name is 
	** and what the current level recording information is.
	*/
	public static string reportDirectoryName = "Simulation Data";
	public static string reportFileName = "report.csv";
	private static string reportSeperator = ",";
	private static string[] reportHeaders = new string[3] {
		"x",
		"y",
		"z"
		};
	public static string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

	
	
	/* The AppendToReport() funciton takes in a string array, normally 3 items, and saves each item
	** in the array to a new column in the .csv file. This function all checks to see if the folder
	** and file exist on the local PC.
	** Parameters:
	**    string[] strings - a string array that will be saved to the .csv file.
	** Return:
	**    N/A
	*/
	public static void AppendToReport(string[] strings) {
		VerifyDirectory();
		VerifyFile();
		using (StreamWriter sw = File.AppendText(GetFilePath())) {
			string finalString = "";
			for (int i = 0; i < strings.Length; i++) {
				if (finalString != "") {
					finalString += reportSeperator;
				}
				finalString += strings[i];
			}
			
			//finalString += GetTimeStamp();
			sw.WriteLine(finalString);
		}
	}
	
	
	
	/* The CreateReport() function checks if the current file folder exist then creates a new .csv file
	** in the folder with x,y, and z as the first items in the colums.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	public static void CreateReport() {
		VerifyDirectory();
		using (StreamWriter sw = File.CreateText(GetFilePath())) {
			string finalString = "";
			for (int i=0; i < reportHeaders.Length; i++) {
				if (finalString != "") {
					finalString += reportSeperator;
				}
				finalString += reportHeaders[i];
			}
			sw.WriteLine(finalString);
		}
	}
	
	
	
	/* The VerifyDirectory() function is used to check if the file folder exist on the desktop, if it
	** doesn't then it calls the CreateDirectory() function.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	static void VerifyDirectory() {
		string dir = GetDirectoryPath();
		
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory(dir);
		}
	}
	
	
	/* The VerifyFile() function is used to check if the file exist in the folder, if it
	** doesn't then it calls the CreateReport() function.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	static void VerifyFile() {
		string fileName = GetFilePath();
		if (!File.Exists(fileName)) {
			CreateReport();
		}
	}
	
	
	/* The GetDirectoryPath() function reads the path to the desktop and adds the custom folder name to the end
	** of the path.
	** Parameters:
	**    N/A
	** Return:
	**    string - the path to the local desktop with the folder name at the end of the path.
	*/
	static string GetDirectoryPath() {
	return DesktopPath + "/" + reportDirectoryName;
	}
	
	
	/* The GetFilePath() function reads the path to the folder on the desktop and adds the custom file name 
	** to the end of the path.
	** Parameters:
	**    N/A
	** Return:
	**    string - the path to the local simulation data folder with the custom file name at the end.
	*/

	static string GetFilePath() {
		return GetDirectoryPath() + "/" + reportFileName;
	}
	
	/* The GetTimeStamp() function reads the local time and returns it as a string
	** Parameters:
	**    N/A
	** Return:
	**    string - the local time
	*/
	static string GetTimeStamp(){
		return System.DateTime.Now.ToString();
	}
}
