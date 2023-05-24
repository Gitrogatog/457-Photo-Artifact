using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageSaver : MonoBehaviour
{
    [SerializeField] private RawImage _frame;
    [SerializeField] private int _maxPhotos;
    private int _photoNum = 0;
    //private Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveImage(){
        _photoNum++;
        if(_photoNum > _maxPhotos){
            _photoNum = 1;
        }
        Texture tex = _frame.texture;
        RenderTexture.active = (RenderTexture)tex;
        Texture2D tex2d = new Texture2D(tex.width, tex.height);
        tex2d.ReadPixels(new Rect(0, 0, tex.width, tex.height),0, 0);
        tex2d.Apply();
        RenderTexture.active = null;
        // Texture2D frameTexture = (Texture2D) _frame.texture;
        // Texture2D _tex2d = new Texture2D(frameTexture.width, frameTexture.height);

        // _tex2d.SetPixels(tex.GetPixels());
        // _tex2d.Apply();

        var bytes = tex2d.EncodeToPNG();
        var dirPath = Application.dataPath + "/Photos/";
        Debug.Log("Rendered image saved to " + dirPath);
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);
        
        File.WriteAllBytes(dirPath + _photoNum + ".png", bytes);
    }
}
