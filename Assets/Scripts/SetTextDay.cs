using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextDay : MonoBehaviour {
    
    Text text;

    public RectRandomizer Invert;
    int prevDay;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = DayManager.Inst.ToFullString();

        if (prevDay != DayManager.Inst.Day) {
            Invert.Randomize();
            prevDay = DayManager.Inst.Day;
        }
    }
}
