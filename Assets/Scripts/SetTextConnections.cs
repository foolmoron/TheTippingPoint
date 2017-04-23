using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextConnections : MonoBehaviour {

    Text text;

    public RectRandomizer Invert;
    int prevConnections = -1;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        if (prevConnections != People.Inst.ConnectedPersons) {
            text.text = string.Format("{0}/{1}", People.Inst.ConnectedPersons, People.Inst.Persons.Length);
            Invert.Randomize();
            prevConnections = People.Inst.ConnectedPersons;
        }
    }
}
