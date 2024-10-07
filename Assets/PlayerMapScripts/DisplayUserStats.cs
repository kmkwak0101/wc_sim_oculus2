using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUserStats : MonoBehaviour
{
	public Text Username;
	public Text DateStatus;
	private string CurrentTime;
	private string PlayerName;
	
	void OnEnable(){
		PlayerName = PlayerPrefs.GetString("Name");
	}
	
    // Start is called before the first frame update
    void Start()
    {
		CurrentTime = System.DateTime.Now.ToString("MM dd yyyy");
		Username.text = PlayerName;
		DateStatus.text = CurrentTime;
        
    }

}
