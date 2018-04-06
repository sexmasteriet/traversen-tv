using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    public RawImage image;
    public AspectRatioFitter fitter;
    
    public void Load(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        float aspectRatio = (float)texture.width / (float)texture.height;

        image.texture = texture;
        fitter.aspectRatio = aspectRatio;
    }
}
