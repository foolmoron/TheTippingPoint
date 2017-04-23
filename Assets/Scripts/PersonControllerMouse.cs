using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonControllerMouse : MonoBehaviour {
    
    PersonController controller;

    void Start() {
        controller = GetComponent<PersonController>();
	}
	
	void Update() {
        if (Input.GetMouseButton(0) && !Menu.Inst.MenuActive) {
            controller.TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else {
            controller.TargetPosition = null;
        }
    }
}
