using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour {

    Rigidbody2D rb;
    public Transform AnimObject;
    public float Speed;
    public float RotateMax;
    public float RotateSpeed;
    public float RotatePhase;
    public Vector2? TargetPosition;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update() {
        // velocity towards 
        {
            if (TargetPosition.HasValue) {
                var vectorToTarget = TargetPosition.Value - rb.position;
                var scale = Mathf.Min(1, vectorToTarget.magnitude * 2);
                rb.velocity = vectorToTarget.normalized * Speed * scale;
            } else {
                rb.velocity = Vector2.zero;
            }
        }
        // animate based on velocity
        {
            if (rb.velocity == Vector2.zero) {
                RotatePhase = 0;
            } else {
                RotatePhase += RotateSpeed * Time.deltaTime;
            }
            AnimObject.localRotation = Quaternion.Euler(-20, 0, Mathf.Sin(RotatePhase) * RotateMax);
        }
	}
}
