using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScreenShotHandler : MonoBehaviour
{
	private static ScreenShotHandler instance;
	private static string filePath = CSVmanager.DesktopPath + "/" + CSVmanager.reportDirectoryName + "/" + CSVmanager.reportFileName;

	private Camera myCamera;
	private bool takeScreenshotOnNextFrame;
	
    // Start is called before the first frame update
	private void Start()
    {
		filePath = filePath.Replace(".csv","");
		filePath = filePath + ".png";
		instance = this;
		myCamera = gameObject.GetComponent<Camera>();
        
    }
	
	private void Awake(){
		instance = this;
		myCamera = gameObject.GetComponent<Camera>();
	}

    // Update is called once per frame
    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame) {
			takeScreenshotOnNextFrame = false;
			RenderTexture renderTexture = myCamera.targetTexture;
			
			Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
			Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
			renderResult.ReadPixels(rect, 0, 0);
			
			byte[] byteArray = renderResult.EncodeToPNG();
			System.IO.File.WriteAllBytes(filePath, byteArray);
			
			RenderTexture.ReleaseTemporary(renderTexture);
			myCamera.targetTexture = null;
		}
    }
	
	private void TakeScreenshot(int width, int height)  {
		myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
		takeScreenshotOnNextFrame = true;
	}
	
	public static void TakeScreenshot_Static(int width, int height) {
		instance.TakeScreenshot(width, height);
	}
}
