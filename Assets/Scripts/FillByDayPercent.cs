using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FillByDayPercent : MonoBehaviour {

    Image image;

    void Awake() {
        image = GetComponent<Image>();
    }

    void Update() {
        image.fillAmount = 1 - (DayManager.Inst.TimeToNextDay / DayManager.Inst.DaySeconds);
    }
}
