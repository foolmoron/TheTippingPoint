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
    [Range(0, 1)]
    public float Saturation;
    [Range(0, 1)]
    public float Brightness;

    void Start() {
	}
	
	void Update() {
        // randomize
        {
            if (Randomize) {
                HeadHue = Random.value;
                ShirtHue = Random.value;
                PantsHue = Random.value;
                var outlineColor = new HSBColor(Random.value, 1, 1).ToColor();
                Head.OutlineColor = outlineColor;
                Shirt.OutlineColor = outlineColor;
                Pants.OutlineColor = outlineColor;
                Randomize = false;
            }
        }
        // colors
        {
            Head.Color = new HSBColor(HeadHue, Saturation, Brightness).ToColor();
            Shirt.Color = new HSBColor(ShirtHue, Saturation, Brightness).ToColor();
            Pants.Color = new HSBColor(PantsHue, Saturation, Brightness).ToColor();
        }
	}
}
