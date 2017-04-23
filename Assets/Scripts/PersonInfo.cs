using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInfo : MonoBehaviour {

    public string FirstName;
    public string LastName;
    public string FullName { get { return FirstName + " " + LastName; } }
    [Range(0, 2)]
    public float Size = 1;

    public bool Randomize;

    Transform anim;

    void Start() {
        anim = transform.FindChild("Anim");
    }

    void Update() {
        if (Randomize) {
            FirstName = NameGenerator.Inst.GetRandomFirstName();
            LastName = NameGenerator.Inst.GetRandomLastName();
            Size = 0.7f + 0.5f * Random.value;
            Randomize = false;
        }
        anim.localScale = new Vector3(Size, Size, 1);
    }
}
