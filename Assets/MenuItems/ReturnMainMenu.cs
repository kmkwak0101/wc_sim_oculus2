using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
	
	public Button startButton;
	private int sceneNumber;
	private int mainMenu = 4;
	
    // Start is called before the first frame update
    void Start()
    {
		startButton.onClick.AddListener(ReturnToMenu);
        
    }

    // Update is called once per frame
	private void ReturnToMenu() {
		SceneManager.LoadScene(mainMenu);
		
	}
}
