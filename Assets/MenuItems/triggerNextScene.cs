using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerNextScene : MonoBehaviour
{
	Collider endLine;
	private int sceneNumber;
	
	
    // Start is called before the first frame update
    void Start()
    {
		endLine = GetComponent<Collider>();
		sceneNumber = SceneManager.GetActiveScene().buildIndex;		
        
    }
	
	void OnTriggerEnter(Collider other) {
		SceneManager.LoadScene(sceneNumber + 1);
	}
}
