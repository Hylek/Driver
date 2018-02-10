﻿using UnityEngine;
using UnityEngine.UI;

public class ColourSwapper : MonoBehaviour
{
    Color color;

    void Start ()
    {
        color = GetComponent<Text>().color;
	}
	
	void Update ()
    {
        GetComponent<Text>().color = Color.Lerp(color, Color.magenta, Mathf.PingPong(Time.time, 1));

	}
}
