using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class Mesh2D : MonoBehaviour {

    public Vector3[] Points;
    public Color Color;

    public bool WithOutline;
    public Color OutlineColor;

    Mesh mesh;
    MeshFilter mf;
    MeshRenderer mr;

    Color[] colors;

    void Awake() {
        mesh = new Mesh();
        mf = GetComponent<MeshFilter>();
        mr = GetComponent<MeshRenderer>();
        mf.mesh = mesh;

        var indices = Triangulator.Triangulate(Points);
        colors = new Color[Points.Length];

        mesh.vertices = Points;
        mesh.triangles = indices;
        mesh.colors = colors;
        mesh.uv = Points.Map(p => new Vector2(1, 1));
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
	
	void Update() {
        if (!Application.isPlaying) {
            Awake();
        }
        for (int i = 0; i < colors.Length; i++) {
            colors[i] = Color;
        }
        mesh.colors = colors;
    }
}
