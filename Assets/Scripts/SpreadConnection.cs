﻿using UnityEngine;
using System.Collections;

public class SpreadConnection : MonoBehaviour {

    public PersonConnection Connection;
    public PersonInfo Info;

    [Range(0, 1)]
    public float Conn2ChanceToConn = 0.33f;
    [Range(0, 1)]
    public float Conn3ChanceToConn = 1f;
    [Range(0, 1)]
    public float Conn3ChanceToConn2 = 0.25f;

    public float SpreadInterval = 2;
    float spreadtime;

    void Awake() {
        spreadtime = Random.value * 5;
    }

    void FixedUpdate() {
        spreadtime -= Time.deltaTime;
        if (spreadtime <= 0) {
            var conn1 = false
                || (Connection.Connection == 2 && Random.value <= Conn2ChanceToConn)
                || (Connection.Connection == 3 && Random.value <= Conn3ChanceToConn)
                ;
            var conn2 = (Connection.Connection == 3 && Random.value <= Conn3ChanceToConn2);
            if (conn1 || conn2) {
                var random = People.Inst.PersonConnections.Random();
                var i = 0;
                while(random.Connection < 2 && i <= 3) {
                    if (i == 3) {
                        foreach (var c in People.Inst.PersonConnections) {
                            if (c.Connection == 0) {
                                random = c;
                                break;
                            }
                        }
                    } else {
                        random = People.Inst.PersonConnections.Random();
                    }
                    i++;
                }
                if (conn1) {
                    if (random.Introducer == null) {
                        random.Introducer = Info;
                    }
                    random.RaiseConnection(1);
                }
                if (conn2) {
                    random.RaiseConnection(2);
                }
            }

            spreadtime = SpreadInterval * (0.5f + Random.value);
        }
    }
}
