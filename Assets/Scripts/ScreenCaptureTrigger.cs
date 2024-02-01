using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Cinemachine;
using System.IO;

public class ScreenCaptureTrigger : MonoBehaviour
{
    public bool screenshotTriggered;
    public string fileName;
    public string filePath;
    public Texture2D screenshotTexture;

    [HideInInspector] public Camera mainCamera;
    private Rect oldPixelRect;

    void Awake()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }
    
    public void CaptureScreen()
    {
        fileName = "screenshot";
        filePath = Path.Combine("Assets/Resources", fileName);

        // If the texture already exists, destroy it and replace with a new one
        if (screenshotTexture != null)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            Destroy(screenshotTexture);
        }

        // Set the camera's pixel rect to match the desired capture size
        oldPixelRect = mainCamera.pixelRect;
        mainCamera.pixelRect = new Rect(0, 0, 3840, 2160);

        // Capture the screenshot and create a texture
        // Wait for end of frame before capturing the screenshot
        StartCoroutine(WaitForEndOfFrameAndCapture());
    }
    
    IEnumerator WaitForEndOfFrameAndCapture()
    {
        yield return new WaitForEndOfFrame();

        // Capture the screenshot and create a texture
        screenshotTexture = ScreenCapture.CaptureScreenshotAsTexture();

        // Write the texture to a file (Vérifier si fonctionne en dehors de l'éditeur)
        byte[] bytes = screenshotTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

        //Sinon, remplacer par 
        // Save the texture in the StreamingAssets folder
        // string texturePath = "Assets/Resources/" + textureName + ".png";
        // AssetDatabase.CreateAsset(newTexture, texturePath);
        // AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();

        Debug.Log("Screenshot saved to: " + filePath);

        // Reset the camera's pixel rect
        mainCamera.pixelRect = oldPixelRect;
    }
}
