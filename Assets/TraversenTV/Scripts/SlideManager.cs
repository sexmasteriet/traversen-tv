using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    Slide current;
    Slide next;

    public Slide slidePrefab;
    public float slideDuration = 1.0f;

    FileInfo currentFile;

    void Start()
    {
        UpdateSlides();
        
        StartCoroutine(UpdateLoop());

        Cursor.visible = false;
    }

    IEnumerator UpdateLoop()
    {
        UpdateSlides();

        yield return new WaitForSeconds(slideDuration);

        StartCoroutine(UpdateLoop());
    }

    void UpdateSlides()
    {
        RefreshCurrentFile();

        if (current)
        {
            Destroy(current.gameObject);
        }

        if (next)
        {
            current = next;
            current.gameObject.SetActive(true);
        }

        next = SpawnSlide(currentFile.FullName);
        next.gameObject.SetActive(false);
    }

    void RefreshCurrentFile()
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
                break;
            }
            if (currentFile.FullName == f.FullName)
            {
                currentFile = files[(i + 1) % files.Length];
                break;
            }
        }
    }

    Slide SpawnSlide(string filePath)
    {
        Slide s = Instantiate(slidePrefab);
        RectTransform t = s.GetComponent<RectTransform>();
        t.SetParent(transform, false);
        t.SetSiblingIndex(0);
        s.Load(filePath);
        return s;
    }
}
