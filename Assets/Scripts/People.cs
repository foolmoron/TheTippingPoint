using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class People : Manager<People> {

    public Rect SpawnArea;
    [Range(0, 1000)]
    public int Count;
    public GameObject PersonPrefab;

    public GameObject[] Persons;
    public PersonConnection[] PersonConnections;
    public PersonInfo[] PersonInfos;
    public PersonColor[] PersonColors;

    public int ConnectedPersons;

    void Awake() {
        Persons = new GameObject[Count];
        PersonConnections = new PersonConnection[Count];
        PersonInfos = new PersonInfo[Count];
        PersonColors = new PersonColor[Count];

        var zeroBasedPositions = Persons.Map(p => new Vector3(SpawnArea.width * (Random.value - 0.5f), SpawnArea.height * (Random.value - 0.5f)));
        zeroBasedPositions = zeroBasedPositions.Map(p => new Vector3(Mathf.Max(Mathf.Abs(p.x), 3) * Mathf.Sign(p.x), Mathf.Max(Mathf.Abs(p.y), 3) * Mathf.Sign(p.y)));
        var center = SpawnArea.center.to3();

        for (int i = 0; i < Persons.Length; i++) {
            Persons[i] = Instantiate(PersonPrefab, zeroBasedPositions[i] + center, Quaternion.identity, transform);
            PersonConnections[i] = Persons[i].GetComponent<PersonConnection>();
            PersonInfos[i] = Persons[i].GetComponent<PersonInfo>();
            PersonColors[i] = Persons[i].GetComponent<PersonColor>();
        }
    }

    void Start() {
        DayManager.Inst.OnNewDay += day => {
            foreach (var connection in PersonConnections) {
                var shouldDowngrade = false
                    || connection.Connection == 3 && (day - connection.DayLastTalked) > connection.Conn3DecayDays
                    || connection.Connection == 2 && (day - connection.DayLastTalked) > connection.Conn2DecayDays
                    || connection.Connection == 1 && (day - connection.DayLastSeen) > connection.Conn1DecayDays
                    ; 
                if (shouldDowngrade) {
                    connection.Connection--;
                    connection.DayLastSeen = day;
                    connection.DayLastTalked = day;
                }
            }
        };
    }

    void Update() {
        ConnectedPersons = PersonConnections.Count(p => p.Connection > 0);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(SpawnArea.center, SpawnArea.size);
    }
}
