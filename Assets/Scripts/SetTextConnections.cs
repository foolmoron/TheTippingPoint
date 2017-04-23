using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextConnections : MonoBehaviour {

    Text text;

    public RectRandomizer Invert;
    int prevConnections;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = string.Format("{0}/{1}", People.Inst.ConnectedPersons, People.Inst.Persons.Length);

        if (prevConnections != People.Inst.ConnectedPersons) {
            Invert.Randomize();
            prevConnections = People.Inst.ConnectedPersons;
        }
    }
}
