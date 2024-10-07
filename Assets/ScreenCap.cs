﻿/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: ScreenCap.cs
** --------
** This program is used to capture the camera view/screen on the simulation when it is called.
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class ScreenCap
{
	//Custom file path to the folder on the user's desktop that contains the player position data
	private static string filePath = CSVmanager.DesktopPath + "/" + CSVmanager.reportDirectoryName + "/" + CSVmanager.reportFileName;
	

	
	/* The TakeScreenshot() funciton is used to capture a screenshot of the main camera/screen during the simultion and it is
	** saved to a folder on the desktop. This is the same folder that the user position data in CVSmanager is saved
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
    public static void TakeScreenshot()
    {
		filePath = filePath.Replace(".csv","");
		filePath = filePath + ".png";
        //ScreenCapture.CaptureScreenshot(filePath);
    }

}
