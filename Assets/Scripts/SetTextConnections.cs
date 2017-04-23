using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextConnections : MonoBehaviour {

    Text text;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = string.Format("{0}/{1}", People.Inst.ConnectedPersons, People.Inst.Persons.Length);
    }
}
