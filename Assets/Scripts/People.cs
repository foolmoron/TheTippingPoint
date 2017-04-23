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

    public PersonConnection[] Persons;

    public int ConnectedPersons;

    void Awake() {
        Persons = new PersonConnection[Count];
        for (int i = 0; i < Persons.Length; i++) {
            Persons[i] = Instantiate(PersonPrefab, new Vector3(SpawnArea.xMin + SpawnArea.width * Random.value, SpawnArea.yMin + SpawnArea.height * Random.value), Quaternion.identity, transform).GetComponent<PersonConnection>();
        }
    }

    void Start() {
        DayManager.Inst.OnNewDay += day => {
            foreach (var person in Persons) {
                var shouldDowngrade = false
                    || person.Connection == 3 && (day - person.DayLastTalked) > person.Conn3DecayDays
                    || person.Connection == 2 && (day - person.DayLastTalked) > person.Conn2DecayDays
                    || person.Connection == 1 && (day - person.DayLastSeen) > person.Conn1DecayDays
                    ; 
                if (shouldDowngrade) {
                    person.Connection--;
                    person.DayLastSeen = day;
                    person.DayLastTalked = day;
                }
            }
        };
    }

    void Update() {
        ConnectedPersons = Persons.Count(p => p.Connection > 0);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(SpawnArea.center, SpawnArea.size);
    }
}
