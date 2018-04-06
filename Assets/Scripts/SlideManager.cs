using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    public RawImage current;
    public RawImage next;

    public float slideDuration = 1.0f;

    FileInfo currentFile;

    void Start()
    {
        UpdateSlides();
        StartCoroutine(UpdateLoop());
    }

    IEnumerator UpdateLoop()
    {
        UpdateSlides();

        yield return new WaitForSeconds(slideDuration);

        StartCoroutine(UpdateLoop());
    }

    void UpdateSlides()
    {
        string path = Path.Combine(Application.persistentDataPath, "Slides");
        DirectoryInfo di = new DirectoryInfo(path);
        FileInfo[] files = di.GetFiles();

        for (int i = 0; i < files.Length; i++)
        {
            FileInfo f = files[i];
            if (currentFile == null)
            {
                currentFile = f;
                print(currentFile);
                break;
            }
            if (currentFile.FullName == f.FullName)
            {
                currentFile = files[(i + 1) % files.Length];
                break;
            }
        }

        if (next.texture)
        {
            current.texture = next.texture;
            AspectRatioFitter fitter = current.GetComponent<AspectRatioFitter>();
            fitter.aspectRatio = (float)current.texture.width / (float)current.texture.height;
        }

        byte[] fileData = File.ReadAllBytes(currentFile.FullName);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        next.texture = texture;

    }
}
