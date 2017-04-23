using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPlayerName : MonoBehaviour {
    
    Text text;

    public RectRandomizer Container;
    string prevFirstName;
    string prevLastName;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        if (prevFirstName != Player.Inst.Info.FirstName || prevLastName != Player.Inst.Info.LastName) {
            text.text = Player.Inst.Info.FullName;
            Container.Randomize();
            prevFirstName = Player.Inst.Info.FirstName;
            prevLastName = Player.Inst.Info.LastName;
        }
    }
}
