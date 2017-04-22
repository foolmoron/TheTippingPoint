using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonOptimizer : MonoBehaviour {

    static Transform MainTarget;

    PersonColor pc;
    GameObject anim;

    public bool IsMain;
    public float ActiveDistance = 5;

    void Awake() {
        pc = GetComponent<PersonColor>();
        anim = transform.FindChild("Anim").gameObject;

        if (IsMain) {
            MainTarget = transform;
        }
    }

    void Update() {
        var active = (MainTarget.position - transform.position).sqrMagnitude < 64;
        pc.enabled = active;
        anim.SetActive(active);
    }
}
