using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInfo : MonoBehaviour {

    public string FirstName;
    public string LastName;
    public string FullName { get { return FirstName + " " + LastName; } }

    public bool Randomize;

    void Start() {

    }

    void Update() {
        if (Randomize) {
            FirstName = NameGenerator.Inst.GetRandomFirstName();
            LastName = NameGenerator.Inst.GetRandomLastName();
            Randomize = false;
        }
    }
}
