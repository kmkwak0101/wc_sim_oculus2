/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: InputManager.cs
** --------
** This Program reads input from the menu screen saved the local PC and Decides the type of input used.
** The input is then read from the user's selected input method and is passed along to the NewCarController.cs script.
*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Uduino;

public class InputManager : MonoBehaviour
{
	public float Throttle, Steer;
	public float forward, down, left, right;
	public bool	brake;
	public static int userInputDecision;
	public GameObject uduinoScriptManager;
	UduinoManager boardManager;
	
	
	
	
	/* The Start() funciton is called first in the progam and is used to create an instance of the Udiono Board Manager that allows the progam to 
	** control the arduino microcontroller. It also defines that pins A0 and A1 on the board will be analog input pins.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	void Start()
    {
		boardManager = UduinoManager.Instance;
		
		UduinoManager.Instance.pinMode(AnalogPin.A0, PinMode.Input);
		UduinoManager.Instance.pinMode(AnalogPin.A1, PinMode.Input);
		UduinoManager.Instance.pinMode(AnalogPin.A2, PinMode.Input);
		UduinoManager.Instance.pinMode(AnalogPin.A3, PinMode.Input);
    }
	
	
	
	
	/* The OnEnable() function pulls the user's input choice off of the local PC and saves it to the gloabl variable userInputDecision.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	void OnEnable(){
		userInputDecision = PlayerPrefs.GetInt("inputType");
		//Debug.Log(userInputDecision);
	}
	
	
	
	
	
    /* The Update() functions is called once per frame and is where the input is read and stored. The if statement handles the different
	** type of inputs, "0" is the universal controller, "1" is for the joystick, and "2" is for the keyboard. In the "0" case the Udion 
	** game object is set to true so that the board can connect to Unity and a packet is sent to read A0 and A1. The ADC values are then
	** converted and saved. The values are assumed to be between 3.6V and 2.4V at the ADC.
	**    N/A
	** Return:
	**    N/A
	*/
    void Update()
    {
		if (userInputDecision == 0) {
			uduinoScriptManager.SetActive(true);
			Throttle = UduinoManager.Instance.analogRead(AnalogPin.A0,"PinRead");
			Steer = UduinoManager.Instance.analogRead(AnalogPin.A1,"PinRead");
			UduinoManager.Instance.SendBundle("PinRead");
			
			//Convert to Voltage scale
			Throttle = (Throttle * 5.0f) / 1023.0f;
			Steer = (Steer * 5.0f) / 1023.0f;
			
			Debug.Log(String.Format("Throttle = {0}, Steer = {1}", Throttle, Steer));
			
			//Normalize values to 1, add .1 to original Throttle Value to account for Arduino error
			Throttle = (Throttle - 2.9f) / 0.6f;
			Steer = (Steer - 2.9f) / 0.6f;
			
			//Debug.Log(String.Format("Throttle = {0}, Steer = {1}", Throttle, Steer));
			if (Throttle > 1.1f || Throttle < -1.1f) {
				Throttle = 0f;
				//Debug.Log(String.Format("Throttle is larger than 3.6V or less than 2.4V: {0}",Throttle));
			}
			else if( Throttle < 0.1f && Throttle > -0.1f) {
				Throttle = 0f;
			}
			
			
			if (Steer > 1.1f || Steer < -1.1f) {
				Steer = 0f;
				//Debug.Log(String.Format("Steer is larger than 3.6V or less than 2.4V: {0}", Steer));
			}
			else if( Steer < 0.1f && Steer > -0.1f) {
				Steer = 0f;
			}
			//Debug.Log(String.Format("Throttle = {0}, Steer = {1}", Throttle, Steer));
			
			brake = Input.GetKey(KeyCode.Space);
		}
		else if (userInputDecision == 1) {
			uduinoScriptManager.SetActive(false);
			
			//Debug.Log("You are now using a Joystick!");
			Throttle = Input.GetAxis("Vertical");
			Steer = Input.GetAxis("Horizontal");
			brake = Input.GetKey(KeyCode.Space);
			
			
		}

		//Code used to hardwire the mouthpiece controller to arduino
		/*
		else if (userInputDecision == 3) {
			
			uduinoScriptManager.SetActive(true);
			forward = UduinoManager.Instance.analogRead(AnalogPin.A0,"PinRead");
			right = UduinoManager.Instance.analogRead(AnalogPin.A1,"PinRead");
			down = UduinoManager.Instance.analogRead(AnalogPin.A2,"PinRead");
			left = UduinoManager.Instance.analogRead(AnalogPin.A3,"PinRead");
			UduinoManager.Instance.SendBundle("PinRead");
			
			if (forward < 100) {
				Throttle = 1;
			}
			else if (down < 100) {
				Throttle = -1;
			}
			else {
				Throttle = 0;
			}
			
			
			
			if (left < 100) {
				Steer = -1;
			}
			else if (right < 100) {
				Steer = 1;
			}
			else {
				Steer = 0;
			}
			
			

			Debug.Log(String.Format("Forward = {0}, Down = {1}, left = {2}, right = {3}", forward, down, left, right));
			
			brake = Input.GetKey(KeyCode.Space);
			
		}
		*/


		else {
			uduinoScriptManager.SetActive(false);
			
			//Debug.Log("You are now using a Joystick!");
			Throttle = Input.GetAxis("Vertical");
			Steer = Input.GetAxis("Horizontal");
			brake = Input.GetKey(KeyCode.Space);
		}  
    }
}
