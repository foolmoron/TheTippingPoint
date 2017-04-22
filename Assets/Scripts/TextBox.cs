using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour {

    TextMesh text;
    Renderer textRenderer;
    Mesh2D mesh;
    
    public Color OutlineColor;

    public bool Reset;
    public Vector3 Scale = new Vector3(1, 1);
    public bool AutoScale = true;
    Vector3 prevScale;

    void Awake() {
        text = transform.FindChild("Text").GetComponent<TextMesh>();
        textRenderer = text.GetComponent<Renderer>();
        mesh = transform.FindChild("Mesh").GetComponent<Mesh2D>();
    }

    float GetRandomScale() {
        return 0.8f + 0.45f * Random.value;
    }

    public void SetText(string desiredText) {
        text.text = desiredText;
    }

    void Update() {
        if (AutoScale) {
            var textSize = textRenderer.bounds.size;
            Scale = new Vector2(0.7f * textSize.x + 0.035f, 0.64f * textSize.y + 0.001f);
        }

        if (prevScale != Scale) {
            Reset = true;
        }
        prevScale = Scale;
        mesh.transform.localScale = Scale;

        mesh.OutlineColor = OutlineColor;

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
