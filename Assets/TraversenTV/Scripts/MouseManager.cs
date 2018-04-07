using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public float hideDelay = 1.0f;
    public float moveThreshold = 0.1f;
	// events
	public UnityEvent mouseShow;
	public UnityEvent mouseHide;

    Vector3 prevMousePos;
    Vector3 mousePos;
    Vector3 deltaMousePos;

    float moveThresholdSqr;

    Coroutine hideCoroutine = null;

    void Start()
    {
        if (!Input.mousePresent)
        {
            gameObject.SetActive(false);
        }
        else
        {
            moveThresholdSqr = moveThreshold * moveThreshold;
        }
    }

    void Update()
    {
        prevMousePos = mousePos;
        mousePos = Input.mousePosition;
        deltaMousePos = prevMousePos - mousePos;

        if (deltaMousePos.sqrMagnitude > moveThresholdSqr)
        {
            Cursor.visible = true;
			mouseShow.Invoke();

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
				hideCoroutine = null;
            }
        }
        else if (hideCoroutine == null)
        {
            hideCoroutine = StartCoroutine(MouseHideDelayed(hideDelay));
        }
    }

    IEnumerator MouseHideDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        Cursor.visible = false;
		mouseHide.Invoke();
        hideCoroutine = null;
    }
}
