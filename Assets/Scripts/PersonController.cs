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

    void Start() {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update() {
        // velocity towards 
        {
            if (Input.GetMouseButton(0)) {
                var vectorToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition).to2() - rb.position;
                var scale = Mathf.Min(1, vectorToMouse.magnitude * 2);
                rb.velocity = vectorToMouse.normalized * Speed * scale;
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
            AnimObject.rotation = Quaternion.Euler(0, 0, Mathf.Sin(RotatePhase) * RotateMax);
        }
	}
}
