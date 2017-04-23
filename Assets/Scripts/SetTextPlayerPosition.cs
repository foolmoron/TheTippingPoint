using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextPlayerPosition : MonoBehaviour {

    Text text;

    public RectRandomizer InvertX;
    public RectRandomizer InvertY;
    int prevX = -1;
    int prevY = -1;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        var x = (int) Player.Inst.transform.position.x;
        var y = (int) Player.Inst.transform.position.y;

        if (x != prevX) {
            text.text = string.Format("[{0:00}, {1:00}]", x, y);
            InvertX.Randomize();
            prevX = x;
        }
        if (y != prevY) {
            text.text = string.Format("[{0:00}, {1:00}]", x, y);
            InvertY.Randomize();
            prevY = y;
        }
    }
}
