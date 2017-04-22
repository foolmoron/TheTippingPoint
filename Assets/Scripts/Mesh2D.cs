using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class Mesh2D : MonoBehaviour {

    public Vector3[] Points;
    public Color Color;

    public bool WithOutline;
    [Range(0, 0.2f)]
    public float OutlineThickness = 0.03f;
    public Vector3 OutlineOffset = new Vector3(0, -0.02f, 0.05f);
    public Color OutlineColor;

    Mesh mesh;
    MeshFilter mf;
    MeshRenderer mr;

    Mesh meshOutline;
    MeshFilter mfOutline;
    MeshRenderer mrOutline;

    Color[] colors;
    Color[] colorsOutline;

    void Awake() {
        mesh = new Mesh();
        mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;
        mr = GetComponent<MeshRenderer>();

        var indices = Triangulator.Triangulate(Points);
        colors = new Color[Points.Length];

        mesh.vertices = Points;
        mesh.triangles = indices;
        mesh.colors = colors;
        mesh.uv = Points.Map(p => new Vector2(1, 1));
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        if (WithOutline) {
            var outline = transform.parent.FindChild(gameObject.name + " (Outline)") ?? new GameObject(gameObject.name + " (Outline)").transform;
            outline.transform.parent = transform.parent;
            outline.transform.localScale = transform.localScale;

            meshOutline = new Mesh();
            mfOutline = outline.gameObject.GetComponent<MeshFilter>() != null ? outline.gameObject.GetComponent<MeshFilter>() : outline.gameObject.AddComponent<MeshFilter>();
            mfOutline.mesh = meshOutline;
            mrOutline = outline.gameObject.GetComponent<MeshRenderer>() != null ? outline.gameObject.GetComponent<MeshRenderer>() : outline.gameObject.AddComponent<MeshRenderer>();
            mrOutline.sharedMaterial = mr.sharedMaterial;

            colorsOutline = new Color[Points.Length];

            var xScale = transform.localScale.x;
            var yScale = transform.localScale.y;
            meshOutline.vertices = Points.Map(p => new Vector3(p.x * (1 + (OutlineThickness / xScale)), p.y * (1 + (OutlineThickness / yScale)), p.z));

            meshOutline.triangles = indices;
            meshOutline.colors = colorsOutline;
            meshOutline.uv = mesh.uv;
            meshOutline.RecalculateNormals();
            meshOutline.RecalculateBounds();
        }
    }
	
	void Update() {
        if (!Application.isPlaying) {
            Awake();
        }
        for (int i = 0; i < colors.Length; i++) {
            colors[i] = Color;
        }
        mesh.colors = colors;
        if (WithOutline) {
            mrOutline.transform.localPosition = transform.localPosition + OutlineOffset;
            mrOutline.transform.localRotation = transform.localRotation;
            for (int i = 0; i < colorsOutline.Length; i++) {
                colorsOutline[i] = OutlineColor;
            }
            meshOutline.colors = colorsOutline;
        }
    }
}
