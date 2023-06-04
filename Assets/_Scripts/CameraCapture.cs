using System.IO;
using UnityEngine;
 
public class CameraCapture : MonoBehaviour
{
    public int fileCounter;
    public KeyCode screenshotKey;
    private RenderTexture rend;
    private Camera Camera
    {
        get
        {
            if (!_camera)
            {
                _camera = Camera.main;
            }
            return _camera;
        }
    }
    private Camera _camera;

    private void Awake(){
        //rend.format = RenderTextureFormat.ARGB32;
        rend = new RenderTexture(Screen.width, Screen.height, 32, RenderTextureFormat.ARGB32);
    }
 
    private void LateUpdate()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            Capture();
        }
    }
 
    public void Capture()
    {
        Camera.targetTexture = rend;
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = Camera.targetTexture;
 
        Camera.Render();
 
        Texture2D image = new Texture2D(Camera.targetTexture.width, Camera.targetTexture.height, TextureFormat.ARGB32, false, true);
        image.ReadPixels(new Rect(0, 0, Camera.targetTexture.width, Camera.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;
 
        byte[] bytes = image.EncodeToPNG();
        Destroy(image);
        var dirPath = Application.dataPath + "/Photos/";
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);
 
        File.WriteAllBytes(dirPath + fileCounter + ".png", bytes);
        fileCounter++;
        Camera.targetTexture = null;
    }
}