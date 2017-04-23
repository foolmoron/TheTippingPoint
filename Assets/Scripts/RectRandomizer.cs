using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RectRandomizer : MonoBehaviour {

    public Vector2 Scales;
    public float Rotation;

    RectTransform rt;

    void Awake() {
        rt = GetComponent<RectTransform>();
    }

    public void Randomize() {
        rt.localScale = new Vector3(1 + Scales.x * (Random.value * 2 - 1), 1 + Scales.y * (Random.value * 2 - 1), 1);
        rt.localRotation = Quaternion.Euler(0, 0, Rotation * (Random.value * 2 - 1));
    }
}
