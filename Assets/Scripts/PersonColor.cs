using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonColor : MonoBehaviour {

    public SpriteRenderer Head;
    public SpriteRenderer Shirt;
    public SpriteRenderer Pants;

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
    [Range(0, 1)]
    public float Alpha;

    void Start() {
	}
	
	void Update() {
        // randomize
        {
            if (Randomize) {
                HeadHue = Random.value;
                ShirtHue = Random.value;
                PantsHue = Random.value;
                Randomize = false;
            }
        }
        // colors
        {
            Head.color = new HSBColor(HeadHue, Saturation, Brightness, Alpha).ToColor();
            Shirt.color = new HSBColor(ShirtHue, Saturation, Brightness, Alpha).ToColor();
            Pants.color = new HSBColor(PantsHue, Saturation, Brightness, Alpha).ToColor();
        }
	}
}
