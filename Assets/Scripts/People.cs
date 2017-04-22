using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour {

    public Rect SpawnArea;
    [Range(0, 1000)]
    public int Count;
    public GameObject PersonPrefab;

    public PersonColor[] Persons;

    void Start() {
        Persons = new PersonColor[Count];
        for (int i = 0; i < Persons.Length; i++) {
            Persons[i] = Instantiate(PersonPrefab, new Vector3(SpawnArea.xMin + SpawnArea.width * Random.value, SpawnArea.yMin + SpawnArea.height * Random.value), Quaternion.identity, transform).GetComponent<PersonColor>();
        }
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(SpawnArea.center, SpawnArea.size);
    }
}
