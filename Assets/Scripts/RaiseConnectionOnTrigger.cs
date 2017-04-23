using UnityEngine;
using System.Collections;

public class RaiseConnectionOnTrigger : MonoBehaviour {

    PersonConnection connection;

    void Awake() {
        connection = transform.parent.GetComponent<PersonConnection>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject != Player.Inst.gameObject) {
            return;
        }
        connection.RaiseConnection(1);
        if (connection.Introducer == null) {
            connection.Introducer = collision.GetComponent<PersonInfo>();
        }
    }
}
