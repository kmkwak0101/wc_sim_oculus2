/* Hanyang University
** Electronic Systems Engineering Technology 
** ESET-420 Capstone II
** Author: SimRacing Studio
** Team: ChairX Tech
** File: SimRacingStudio.cs
** --------
** This Program calls the Telemtry.cs program to read the telemetry data from the vehicle in Unity and maps all the data
** to variable that the SimRacing Studio application can interprit and read. This program also opens a UDP connection on a
** user defined port with the SimRacing Studion Application to send live telemetry updates. This is part of the SimRacing 
** Studio API used to work with unity, provided by SimRacing Studio.
*/




using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using AddressFamily = System.Net.Sockets.AddressFamily;
using System.Runtime.InteropServices;


/* The structure crated in the Telemtry.cs program is further defined here and used to consolidate the telemetry data
** sent to the SimRacing Studio application.
** Parameters:
**    N/A
** Return:
**    N/A
*/

[StructLayout(LayoutKind.Sequential)]
public struct telemetryPacket
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public char[] apiMode;
    public uint version;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
    public char[] game;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
    public char[] vehicleName;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
    public char[] location;
    public float speed;
    public float rpm;
    public float maxRpm;
    public int gear;
    public float pitch;
    public float roll;
    public float yaw;
    public float lateralVelocity;
    public float lateralAcceleration;
    public float verticalAcceleration;
    public float longitudinalAcceleration;
    public float suspensionTravelFrontLeft;
    public float suspensionTravelFrontRight;
    public float suspensionTravelRearLeft;
    public float suspensionTravelRearRight;
    public uint wheelTerrainFrontLeft;
    public uint wheelTerrainFrontRight;
    public uint wheelTerrainRearLeft;
    public uint wheelTerrainRearRight;

    public telemetryPacket(char[] pmode, uint pversion, char[] pgame, char[] pvehicleName, char[] plocation, float pspeed, float prpm, float pmaxRpm, int pgear, 
	float ppitch, float proll, float pyaw, float plateralVelocity, float plateralAcceleration, float pverticalAcceleration, float plongitudinalAcceleration, 
	float psuspensionTravelFrontLeft, float psuspensionTravelFrontRight, float psuspensionTravelRearLeft, float psuspensionTravelRearRight, uint pwheelTerrainFrontLeft, 
	uint pwheelTerrainFrontRight, uint pwheelTerrainRearLeft, uint pwheelTerrainRearRight)
    {
        apiMode = pmode;
        version = pversion;
        game = pgame;
        vehicleName = pvehicleName;
        location = plocation;
        speed = pspeed;
        rpm = prpm;
        maxRpm = pmaxRpm;
        gear = pgear;
        pitch = ppitch;
        roll = proll;
        yaw = pyaw;
        lateralVelocity = plateralVelocity;
        lateralAcceleration = plateralAcceleration;
        verticalAcceleration = pverticalAcceleration;
        longitudinalAcceleration = plongitudinalAcceleration;
        suspensionTravelFrontLeft = psuspensionTravelFrontLeft;
        suspensionTravelFrontRight = psuspensionTravelFrontRight;
        suspensionTravelRearLeft = psuspensionTravelRearLeft;
        suspensionTravelRearRight = psuspensionTravelRearRight;
        wheelTerrainFrontLeft = pwheelTerrainFrontLeft;
        wheelTerrainFrontRight = pwheelTerrainFrontRight;
        wheelTerrainRearLeft = pwheelTerrainRearLeft;
        wheelTerrainRearRight = pwheelTerrainRearRight;
    }
}




[ExecuteInEditMode]
public class SimRacingStudio : MonoBehaviour
{

    //public string srsHostIP = "127.0.0.1";  //to broadcast to all computers in the network use "0.0.0.0"
    private string srsHostIP = "127.0.0.1";
    public int srsHostPort = 33001;  //this port can be changed on the config.ini of srs app
    IPEndPoint remoteEndPoint;
    static UdpClient udpClient;
    static telemetryPacket tp;


	/* The Start() funciton calls the rest of the funcions used in the program on the start of the simulation and creates the UDP
	** tunnel.
	** Parameters:
	**    N/A
	** Return:
	**    N/A
	*/

    void Start()
    {
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(srsHostIP), srsHostPort);
        udpClient = new UdpClient();
        tp = new telemetryPacket();
        StartCoroutine("SimRacingStudio_Start");
    }
	
	
	
	/* The SimRacingStudio_SendTelemetry() function is the actual funciton that calls the telemtry structure and uses it to pull the telemtry data from 
	** unity.
	** Parameters:
	**    pMode - api Version
	**    pVersion - api Version
	**    pgame - name of game
	**    pvehicleName - name of vehivle
	**    plocation - name of game location
	**    pspeed - player speed
	**    prpm - vehivle RPM (Not used for this simulation)
	**    pmaxRpm - max RPM available
	**    pgear - which gear the vehicle is in (Not used for this simulation)
	**    ppitch - pith of the vehicle
	**    proll - roll of the vehicle
	**    pyaw - yaw of the vehicle
	**    plateralVelocity - lateral velocity of the vehicle
	**    plateralAcceleration - lateral acceleration of the vehicle
	**    pverticalAcceleration - vertical acceleration of the vehicle
	**    plongitudinalAcceleration - longitudal acceleration of the vehicle
	**    psuspensionTravelFrontLeft - front left suspension travel for the vehicle
	**    psuspensionTravelFrontRight - front right suspension travel for the vehicle
	**    psuspensionTravelRearLeft - rear left suspension travel for the vehicle
	**    psuspensionTravelRearRight - rear right suspension travel for the vehicle
	**    pwheelTerrainFrontLeft - type of terrain that the front left wheel is travelling over
	**    pwheelTerrainFrontRight - type of terrain that the front Right wheel is travelling over
	**    pwheelTerrainRearLeft - type of terrain that the Rear left wheel is travelling over
	**    pwheelTerrainRearRight - type of terrain that the Rear Right wheel is travelling over
	** Return:
	** N/A
	*/
    public static void SimRacingStudio_SendTelemetry(
	char[] pMode, uint pversion, char[] pgame, char[] pvehicleName, char[] plocation, float pspeed, float prpm, float pmaxRpm, int pgear, float ppitch, float proll, 
	float pyaw, float plateralVelocity, float plateralAcceleration, float pverticalAcceleration, float plongitudinalAcceleration, float psuspensionTravelFrontLeft, 
	float psuspensionTravelFrontRight, float psuspensionTravelRearLeft, float psuspensionTravelRearRight, uint pwheelTerrainFrontLeft, uint pwheelTerrainFrontRight, 
	uint pwheelTerrainRearLeft, uint pwheelTerrainRearRight)
    {
        tp = new telemetryPacket(pMode, pversion, pgame, pvehicleName, plocation, pspeed, prpm, pmaxRpm, pgear, ppitch, proll, pyaw, plateralVelocity, plateralAcceleration, 
		pverticalAcceleration, plongitudinalAcceleration, psuspensionTravelFrontLeft, psuspensionTravelFrontRight, psuspensionTravelRearLeft, psuspensionTravelRearRight, 
		pwheelTerrainFrontLeft, pwheelTerrainFrontRight, pwheelTerrainRearLeft, pwheelTerrainRearRight);
    }




	/* A enumator to iterate through the data being colected and send each telemtry packet to the SimRacing Studio Application through a UDP tunnel.
	** Parameters:
	**    N/A
	** Return:
	**    null
	*/
    IEnumerator SimRacingStudio_Start()
    {
        while (true)
        {

            int size = Marshal.SizeOf(tp);
            byte[] packet = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(tp, ptr, true);
            Marshal.Copy(ptr, packet, 0, size);

			//Debug.Log("Sending: " + tp.version);
			//Debug.Log(String.Format("sending... Pitch {0}, Roll {1}, and Yaw {2}",tp.pitch, tp.roll,tp.yaw));

            udpClient.Send(packet, packet.Length, remoteEndPoint);

            yield return null;


        }
    }
	
	
	/* The LocalIPAddress() function returns a string value when callled. The string should be the local IP of the host PC when it is connected to
	** the internet. The returned IP should also be an IPv4 private network IP.
	** Parameters:
	**    N/A
	** Return:
	**    A IPv4 address converted to a string.
	*/
	public static string LocalIPAddress()
    {
		 IPHostEntry host;
		 string localIP = "0.0.0.0";
		 string[] IPSeparate;
		 host = Dns.GetHostEntry(Dns.GetHostName());
		 foreach (IPAddress ip in host.AddressList)
		 {
			 if (ip.AddressFamily == AddressFamily.InterNetwork)
			 {
				 localIP = ip.ToString();
				 IPSeparate = localIP.Split(new char[] {'.'});
				 if (IPSeparate[0] == "10"){
					Debug.Log(localIP);
					break;
				 }
				 else if (IPSeparate[0] == "172") {
					Debug.Log(localIP);
					break;
				 }
				 else if (IPSeparate[0] == "192") {
					Debug.Log(localIP);
					break;
				 }
			 }
		 }
		 return localIP;
    }

}