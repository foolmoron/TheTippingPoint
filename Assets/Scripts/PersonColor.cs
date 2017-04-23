using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonColor : MonoBehaviour {

    public Mesh2D Head;
    public Mesh2D Shirt;
    public Mesh2D Pants;

    public bool Randomize;
    [Range(0, 1)]
    public float HeadHue;
    [Range(0, 1)]
    public float ShirtHue;
    [Range(0, 1)]
    public float PantsHue;
    public Color OutlineColor = Color.black;
    [Range(0, 1)]
    public float Saturation;
    [Range(0, 1)]
    public float Brightness;
    public bool FadeColor;
    public bool FadeOutline;

    void Awake() {
        if (Randomize) {
            HeadHue = Random.value * 0.99f;
            ShirtHue = Random.value * 0.99f;
            PantsHue = Random.value * 0.99f;
            OutlineColor = new HSBColor(Random.value * 0.99f, 1, 1).ToColor();
            Randomize = false;
        }
    }

    void Start() {
	}
	
	void Update() {
        // randomize
        {
            if (Randomize) {
                HeadHue = Random.value * 0.99f;
                ShirtHue = Random.value * 0.99f;
                PantsHue = Random.value * 0.99f;
                OutlineColor = new HSBColor(Random.value * 0.99f, 1, 1).ToColor();
                Randomize = false;
            }
        }
        // colors
        {
            Head.Color = new HSBColor(HeadHue, (FadeColor ? 0.33f : 1) * Saturation, (FadeColor ? 0.5f : 1) * Brightness).ToColor();
            Shirt.Color = new HSBColor(ShirtHue, (FadeColor ? 0.33f : 1) * Saturation, (FadeColor ? 0.5f : 1) * Brightness).ToColor();
            Pants.Color = new HSBColor(PantsHue, (FadeColor ? 0.33f : 1) * Saturation, (FadeColor ? 0.5f : 1) * Brightness).ToColor();
            Head.OutlineColor = FadeOutline ? Color.gray : OutlineColor;
            Shirt.OutlineColor = FadeOutline ? Color.gray : OutlineColor;
            Pants.OutlineColor = FadeOutline ? Color.gray : OutlineColor;
        }
	}
}
