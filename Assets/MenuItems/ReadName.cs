﻿/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: ReadName.cs
** --------
** This program is used to record the name of the user and save it to the local PC.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ReadName : MonoBehaviour
{
	public Text FirstNameInput;
	public Text LastNameInput;
	public string FirstName;
	public string LastName;
	

	/* The Update() function is called once per frame and is used to read what the user is typing
	** into the input field on the menu.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
    void Update()
    {
		FirstName = FirstNameInput.text.ToString();
		LastName = LastNameInput.text.ToString();  
    }
	
	
	/* The OnDisable() function is called when the script is closed and saves the user's name to the 
	** local PC.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	void OnDisable(){
		PlayerPrefs.SetString("Name", FirstName + "_" + LastName);
	}
}
