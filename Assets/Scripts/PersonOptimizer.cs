using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonOptimizer : MonoBehaviour {

    static Transform MainTarget;

    public MonoBehaviour[] componentsToDisable;
    public GameObject[] objectsToDisable;

    public bool IsMain;
    public float ActiveDistance = 5;

    void Awake() {
        if (IsMain) {
            MainTarget = transform;
        }
    }

    void Update() {
        var active = (MainTarget.position - transform.position).sqrMagnitude < 64;
        foreach (var c in componentsToDisable) {
            c.enabled = active;
        }
        foreach (var o in objectsToDisable) {
            o.SetActive(active);
        }
    }
}
