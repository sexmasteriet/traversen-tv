using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Clock : MonoBehaviour
{
	DateTime now;

	Text text;

	void Start()
	{
		text = GetComponent<Text>();
	}

    void Update()
    {
		now = DateTime.Now;

		text.text = now.ToString("HH:mm");
    }
}
