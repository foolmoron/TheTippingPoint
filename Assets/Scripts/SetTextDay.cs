using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextDay : MonoBehaviour {

    Text text;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = DayManager.Inst.ToFullString();
    }
}
