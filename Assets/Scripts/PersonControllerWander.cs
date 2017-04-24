using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonControllerWander : MonoBehaviour {

    public float TargetInterval;
    float targetTime;

    PersonController controller;

    void Start() {
        controller = GetComponent<PersonController>();
	}
	
	void FixedUpdate() {
        if (targetTime <= 0 && DayManager.Inst.Started) {
            controller.TargetPosition = transform.position.to2() + Random.insideUnitCircle * (1f + 2f * Random.value);
            targetTime = TargetInterval * (0.5f + Random.value);
        }
        targetTime -= Time.deltaTime;
    }
}
