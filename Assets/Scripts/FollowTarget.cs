using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform Target;
    public float Speed = 0.9f;

    void Start() {
	}
	
	void Update() {
        transform.position = Vector2.Lerp(transform.position.to2(), Target.position.to2(), Speed).to3(transform.position.z);
	}
}
