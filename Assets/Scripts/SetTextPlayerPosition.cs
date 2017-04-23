using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextPlayerPosition : MonoBehaviour {

    Text text;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = string.Format("[{0:00}, {1:00}]", Player.Inst.transform.position.x, Player.Inst.transform.position.y);
    }
}
