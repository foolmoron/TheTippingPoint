using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnableOnGameOver : MonoBehaviour {

    float timer = 2;

    void Awake() {
    }

    void Update() {
        timer -= Time.deltaTime;
    }

    public void Disable() {
        if (timer <= 0) {
            gameObject.SetActive(false);
        }
    }
}
