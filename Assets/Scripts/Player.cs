using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Manager<Player> {

    public PersonInfo Info;
    public PersonColor Color;

    void Awake()
    {
        Info = GetComponent<PersonInfo>();
        Color = GetComponent<PersonColor>();
    }
}
