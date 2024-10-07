/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: textTest.cs
** --------
** This Program takes the users input choice and displays it on the screen of the simulation, used to debug input selection.
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class textTest : MonoBehaviour
{
   public Text userChoiceText;
   private int userInputNumber;
   private string userChoiceStr;

    // Update is called once per frame
    void Update()
    {
		userInputNumber = InputManager.userInputDecision;
		
		if (userInputNumber == 0) {
			userChoiceStr = "Universal Controller";
		}
		else if (userInputNumber == 1){
			userChoiceStr = "Joystick";
		}
		else if (userInputNumber == 3) {
			userChoiceStr = "Mouth Piece";
		}
		else {
				userChoiceStr = "Keyboard";
		}
		
       userChoiceText.text =  userChoiceStr;
    }
}
