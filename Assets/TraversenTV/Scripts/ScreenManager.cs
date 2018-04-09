using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenManager : MonoBehaviour
{
    public UnityEvent fullscreenEnter;
    public UnityEvent fullscreenExit;

    void Update()
    {
		if (Screen.fullScreen)
        {
            fullscreenEnter.Invoke();
        }
        else
        {
            fullscreenExit.Invoke();
        }
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen)
        {
            fullscreenEnter.Invoke();
        }
        else
        {
            fullscreenExit.Invoke();
        }
    }
}
