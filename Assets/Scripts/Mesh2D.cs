using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class Mesh2D : MonoBehaviour {

    public Vector3[] Points;
    public Color Color = Color.white;

    Mesh mesh;
    MeshFilter mf;
    MeshRenderer mr;
    Color[] colors;

    public bool WithOutline;
    [Range(0, 0.2f)]
    public float OutlineThickness = 0.03f;
    public Vector3 OutlineOffset = new Vector3(0, -0.02f, 0.05f);
    public Color OutlineColor = Color.black;

    Vector3[] originalPointsOutline;
    Vector3[] currentPointsOutline;
    Vector3[] desiredPointsOutline;
    Mesh meshOutline;
    MeshFilter mfOutline;
    MeshRenderer mrOutline;
    Color[] colorsOutline;
    
    [Range(0, 1f)]
    public float OutlineShake = 0.3f;

    void Awake() {
        mesh = new Mesh();
        mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;
        mr = GetComponent<MeshRenderer>();

        Reset();
    }

    public void Reset() {
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

            meshOutline = meshOutline ?? new Mesh();
            mfOutline = outline.gameObject.GetComponent<MeshFilter>() != null ? outline.gameObject.GetComponent<MeshFilter>() : outline.gameObject.AddComponent<MeshFilter>();
            mfOutline.mesh = meshOutline;
            mrOutline = outline.gameObject.GetComponent<MeshRenderer>() != null ? outline.gameObject.GetComponent<MeshRenderer>() : outline.gameObject.AddComponent<MeshRenderer>();
            mrOutline.sharedMaterial = mr.sharedMaterial;

            colorsOutline = colorsOutline ?? new Color[Points.Length];

            var xScale = transform.lossyScale.x;
            var yScale = transform.lossyScale.y;
            originalPointsOutline = Points.Map(p => new Vector3(p.x * (1 + (OutlineThickness / xScale)), p.y * (1 + (OutlineThickness / yScale)), p.z), originalPointsOutline);
            desiredPointsOutline = Points.Map(p => p, desiredPointsOutline);
            if (OutlineShake > 0) {
                for (int i = 0; i < desiredPointsOutline.Length; i++) {
                    desiredPointsOutline[i] = originalPointsOutline[i] * (0.98f + OutlineShake * Random.value);
                }
            }
            currentPointsOutline = desiredPointsOutline.Map(p => p, currentPointsOutline);

            meshOutline.vertices = currentPointsOutline;
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
        // colors
        for (int i = 0; i < colors.Length; i++) {
            colors[i] = Color;
        }
        mesh.colors = colors;
        // outline colors
        if (WithOutline) {
            mrOutline.transform.localPosition = transform.localPosition + OutlineOffset;
            mrOutline.transform.localRotation = transform.localRotation;
            for (int i = 0; i < colorsOutline.Length; i++) {
                colorsOutline[i] = OutlineColor;
            }
            meshOutline.colors = colorsOutline;

            // outline shake
            if (OutlineShake > 0) {
                // random new desired pos
                for (int i = 0; i < desiredPointsOutline.Length; i++) {
                    if (Random.value < 0.25f) {
                        desiredPointsOutline[i] = originalPointsOutline[i] * (0.98f + OutlineShake * Random.value);
                    }
                }
                // lerp current to desired
                for (int i = 0; i < currentPointsOutline.Length; i++) {
                    currentPointsOutline[i] = Vector3.MoveTowards(currentPointsOutline[i], desiredPointsOutline[i], 5 * OutlineShake * Time.deltaTime);
                }
                // set current verts
                meshOutline.vertices = currentPointsOutline;
            } else {
                meshOutline.vertices = originalPointsOutline;
            }
        }
    }
}
