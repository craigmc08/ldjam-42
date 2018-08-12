﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour {
    public string prefix;
    public string suffix;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        string txt = prefix + GameController.Score + suffix;
        text.text = txt;
    }
}
