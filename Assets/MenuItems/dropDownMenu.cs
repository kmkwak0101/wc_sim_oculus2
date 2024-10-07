/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: dropDownMenu.cs
** --------
** This program checks to see what the user has selected on the menu drop down selector. Depending on the option selected a value is
** saved to the local computer.
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dropDownMenu : MonoBehaviour
{
	private int userInputDecision;
	
	public void handleInputSelection(int val) {
		if (val == 0) {
			//Debug.Log("Universal Controller");
			userInputDecision = 0;
		}
		if (val == 1) {
			//Debug.Log("Joystick");
			userInputDecision = 1;
		}
		if (val == 2) {
			//Debug.Log("keyboard");
			userInputDecision = 2;
		}
		if (val == 3) {
			userInputDecision = 3;
		}
	}
	
	void OnDisable(){
		PlayerPrefs.SetInt("inputType", userInputDecision);
	}
}
