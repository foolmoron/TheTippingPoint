using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    Animator animator;

    public bool MenuActive;
    bool prevActive;
    
    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void ToggleMenu() {
        MenuActive = !MenuActive;
    }

    void Update() {
        if (prevActive != MenuActive) {
            animator.Play(MenuActive ? "MenuEnter" : "MenuExit");
            prevActive = MenuActive;
        }
    }
}
