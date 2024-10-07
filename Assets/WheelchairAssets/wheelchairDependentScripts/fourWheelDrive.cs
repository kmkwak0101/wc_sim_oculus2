﻿/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: Alejandro Almodovar
** Team: ChairX Tech
** File: fourWheelDrive.cs
** --------
** This program is used to control the wheelchair rigidbody used in the simulation. Input is taken from the InputManager.cs
** program.
*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Uduino;


[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
public class fourWheelDrive : MonoBehaviour
{

	
	public InputManager IM;
	public List<WheelCollider> throttleWheels;
	public List<GameObject> steeringWheels;
	public float strengthCoeficient = 5000f;
	public float maxTurn = 5f;
	public Transform CM;
	public Rigidbody rb;
	public float brakeStrength;
	public bool isOnGround;
	private float maxSpeed = 3.0f;
	private Vector3 wheelchairSpeed;
	
	
	
	/* The Start() function is called fist and maps the input manger and the rigidbody to global variables.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	void Start()
    {
		IM = GetComponent<InputManager>();
		rb = GetComponent<Rigidbody>();
    }
	
	
	/* The FixedUpdate() function is called once per frame to call the movement() and steering() funtions, ability to add other vehicle controls here.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
    void FixedUpdate()
    {
		movement();
		steering();	
    }
	
	
	/* The steering() function is used to change the angle of the  steering wheels on the vechicle in Unity. This funciton also updates 
	** the position of the wheelcolliders to match the steering angle applied.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	void steering() {
		
		foreach (GameObject wheel in steeringWheels) {
			wheel.GetComponent<WheelCollider>().steerAngle = IM.Steer * maxTurn;
			wheel.transform.localEulerAngles = new Vector3(0f, IM.Steer * maxTurn, 0f);
		}
		
	}
	
	
	/* The movement() function is used to give torque and movment to the wheelcolliders on the vehicle in unity. It also handles turning
	** by rotating the entire rigidbody. To slow the vehicle down more naturaly the vehicle's velocity is reduced by 15% every frame.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	void movement() {
		float pitchVehicle = (rb.rotation * Quaternion.Euler(180, 0, 0)).eulerAngles.x;
		//float yawVehicle = (rb.rotation * Quaternion.Euler(0,180,0)).eulerAngles.z;
		//Debug.Log(yawVehicle);
		if(rb.velocity.magnitude > maxSpeed){
             rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
		}
		foreach (WheelCollider wheel in throttleWheels) {	
			if (IM.brake) {
				wheel.motorTorque = 0f;
				wheel.brakeTorque = brakeStrength * Time.deltaTime; 
			}
			else if (IM.Throttle == 0){
				if (IM.Steer > 0) {
					//rb.velocity = new Vector3(0f,0f,0f);
					rb.transform.Rotate(0f, 0.25f, 0f,Space.Self);
					}
				else if (IM.Steer < 0) {
					//rb.velocity = new Vector3(0f,0f,0f);
					rb.transform.Rotate(0f, -0.25f, 0f,Space.Self);
				}
				else {
					if (pitchVehicle > 1f) {
						wheelchairSpeed = rb.velocity;
						//wheelchairSpeed = wheelchairSpeed * 0.25f;
						rb.velocity = wheelchairSpeed;
					}
					else {
						wheelchairSpeed = rb.velocity;
						wheelchairSpeed = wheelchairSpeed * 0.85f;
						rb.velocity = wheelchairSpeed;
					}
				}
			}
			else {
				wheel.motorTorque = strengthCoeficient * Time.deltaTime * IM.Throttle;
				wheel.brakeTorque = 0f;
			}
		}	
	}
}
