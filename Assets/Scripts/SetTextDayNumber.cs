using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextDayNumber : MonoBehaviour {
    
    Text text;

    public RectRandomizer Container;
    int prevDay = -1;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        if (prevDay != DayManager.Inst.Day) {
            text.text = "Day " + DayManager.Inst.Day;
            Container.Randomize();
            prevDay = DayManager.Inst.Day;
        }
    }
}
