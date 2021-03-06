﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextDay : MonoBehaviour {
    
    Text text;

    public RectRandomizer Invert;
    int prevDay = -1;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {

        if (prevDay != DayManager.Inst.Day) {
            text.text = DayManager.Inst.ToFullString();
            Invert.Randomize();
            prevDay = DayManager.Inst.Day;
        }
    }
}
