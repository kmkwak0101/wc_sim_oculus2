/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: SimRacing Studio/Alejandro Almodovar
** Team: ChairX Tech
** File: Telemtry.cs
** --------
** This Program creates a custom structure that is called by the SimRacingStudio.cs program to read and pass along telemetry
** data to the SimRacing Studio application that controls the motion platform movements. The majority of this code was 
** provided by SimRacing Studio but it was appened to fix errors in the pitch, roll, and yaw data found during simulation testing.
*/


using UnityEngine;
using System;

[ExecuteInEditMode]
public class Telemetry : MonoBehaviour
{

    string apiMode = "api";  //constant to identify the package
    public string game = "Project Cars 2";  //constant to identify the game
    public string vehicle = "Lamborghini Huracan";  //constant to identify the vehicle
    public string location = "Circuit Gilles-Villeneuve";  //constant to identify the location
    uint apiVersion = 102;  //constant of the current version of the api
	Rigidbody vehicleBody;
	private float FX, FY, FZ;
	
	
	/* The Start() function is used to map the unity vehicle to the rigidBody global variable.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/

    void Start ()
    {
        vehicleBody = GetComponent<Rigidbody> ();
    }

    
	
	/* The Update() function is called once per frame and is where the actual structure of the data being sent to the SimRacingStudio.cs
	** is formed and read. The data is the basic telemetry for the rigidBody vehicle and other parameters such as speed, rpm, etc. which
	** is further defined in the SimRacingStudio.cs program.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/
	
    void Update()
    {
		FX = FixX();
		FY = FixY();
		FZ = FixZ();

        SimRacingStudio.SimRacingStudio_SendTelemetry(apiMode.PadRight(50).ToCharArray()
                                                     , apiVersion
                                                     , game.PadRight(50).ToCharArray()
                                                     , vehicle.PadRight(50).ToCharArray()
                                                     , location.PadRight(50).ToCharArray()
                                                     , Convert.ToSingle(vehicleBody.velocity.magnitude * 3.6)
                                                     , 7000
                                                     , 8000
                                                     , -1
                                                     , FX
                                                     , FY
                                                     , FZ
                                                     , 0
                                                     , 0
                                                     , 0
                                                     , 0
                                                     , 0
                                                     , 0
                                                     , 0
                                                     , 0
                                                     , 0 
                                                     , 0
                                                     , 0
                                                     , 0);
    }
	
	
	
	
	
	
	
	/* The FixY(), FixX(), and FixZ() function are the custom functions developed to fixed the error when trying to read pitch, roll, and yaw.
	** They create a vector that is then compared to a built in vector for the given rigidbody and returns the delta. The delta is the actual 
	** value needed when telling the motion platform what to do. The original code passed along an incorect vector value to the SimRacingStudio.cs
	** program.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	** Notes: 
	**    Explination for FixX():
	**       Multiplies the difference between fwd and transform.forward by the sign of transform.forward.y
	**       Vector3.Angle takes the difference between the fwd vector and transform.forward vector
	**       fwd vector is relative to the rigidbody and transform.foward is relative to the world
	*/

	float FixY() {
		var fwd = transform.forward;
		fwd.y = 0;
		fwd *= Mathf.Sign(transform.up.y);
		var right = Vector3.Cross(Vector3.up, fwd).normalized;
		float roll = Vector3.Angle(right, transform.right) * Mathf.Sign(transform.right.y);
		//Debug.Log(roll);
		return roll;
	}
	
	float FixX() {
		var right = transform.right;
		right.y = 0;
		right *= Mathf.Sign(transform.up.y);
		var fwd = Vector3.Cross(right, Vector3.up).normalized;
		float pitch = Vector3.Angle(fwd, transform.forward) * Mathf.Sign(transform.forward.y);
		//Debug.Log(String.Format("fwd = {0}, transform.forward = {1}", fwd, transform.forward));
		if (pitch > 14f) {
			pitch = 50f;
		}
		else if (pitch < -14f){
			pitch = -50f;
		}
		
		return pitch;
	}
	
	float FixZ() {
		float yaw = (vehicleBody.rotation * Quaternion.Euler(0,180,0)).eulerAngles.z;
		return yaw;
	}
}
