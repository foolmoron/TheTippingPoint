using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour {

    Mesh2D mesh;

    public bool Reset;
    Vector3 prevScale;

    void Start() {
        mesh = transform.FindChild("Mesh").GetComponent<Mesh2D>();
    }

    float GetRandomScale() {
        return 0.8f + 0.45f * Random.value;
    }

    void Update() {
        if (prevScale != transform.localScale) {
            Reset = true;
        }
        prevScale = transform.localScale;

        if (Reset) {
            mesh.Points[0] = new Vector3(-1 * GetRandomScale(), 1 * GetRandomScale(), 0);
            mesh.Points[1] = new Vector3(1 * GetRandomScale(), 1 * GetRandomScale(), 0);
            mesh.Points[2] = new Vector3(1 * GetRandomScale(), -1 * GetRandomScale(), 0);
            mesh.Points[3] = new Vector3(-1 * GetRandomScale(), -1 * GetRandomScale(), 0);
            mesh.Reset();
            Reset = false;
        }
    }
}
