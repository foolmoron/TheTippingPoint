using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextPlayerPosition : MonoBehaviour {

    Text text;

    public RectRandomizer InvertX;
    public RectRandomizer InvertY;
    int prevX;
    int prevY;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        var x = (int) Player.Inst.transform.position.x;
        var y = (int) Player.Inst.transform.position.y;
        text.text = string.Format("[{0:00}, {1:00}]", x, y);

        if (x != prevX) {
            InvertX.Randomize();
            prevX = x;
        }
        if (y != prevY) {
            InvertY.Randomize();
            prevY = y;
        }
    }
}
