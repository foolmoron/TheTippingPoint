using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonConnection : MonoBehaviour {

    [Range(0, 3)]
    public int Connection;

    public PersonInfo Introducer;

    PersonColor color;
    Mesh2D[] meshes;

    void Awake() {
        color = GetComponent<PersonColor>();
        meshes = GetComponentsInChildren<Mesh2D>();
    }
    
    public void RaiseConnection(int newConnection) {
        Connection = Mathf.Max(Connection, newConnection);
    }

    void Update() {
        color.FadeOutline = Connection < 1;
        color.FadeColor = Connection < 2;
        foreach (var mesh in meshes) {
            mesh.OutlineShake = Connection >= 3 ? 0.8f : Connection >= 2 ? 0.15f : 0;
        }
    }
}
