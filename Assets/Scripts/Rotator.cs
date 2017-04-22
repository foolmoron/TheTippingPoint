using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public Vector3 RotationVelocity;
    
    void Update() {
        transform.Rotate(RotationVelocity * Time.deltaTime);
    }
}
