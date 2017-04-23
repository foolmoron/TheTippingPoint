using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PersonConnection : MonoBehaviour {

    [Range(0, 3)]
    public int Connection;

    public PersonInfo Introducer;

    public int DayLastSeen;
    public int DayLastTalked;

    public int Conn1DecayDays = 5;
    public int Conn2DecayDays = 5;
    public int Conn3DecayDays = 5;

    PersonColor color;
    Mesh2D[] meshes;

    PersonController controller;
    ContactedPlayer contact;

    void Awake() {
        color = GetComponent<PersonColor>();
        meshes = GetComponentsInChildren<Mesh2D>();

        controller = GetComponent<PersonController>();

        contact = GetComponentInChildren<ContactedPlayer>();
        contact.OnSeen += OnSeen;
        contact.OnTalk1 += OnTalk1;
        contact.OnTalk2 += OnTalk2;
        contact.OnTalk3 += OnTalk3;
    }

    public static void OnSeen(ContactedPlayer contact) {
        contact.Connection.DayLastSeen = DayManager.Inst.Day;
    }

    public static void OnTalk1(ContactedPlayer contact) {
        contact.Connection.DayLastTalked = DayManager.Inst.Day;
        contact.TextHelper.ShowText("Hi ", T.PLAYER_FIRSTNAME, "! I'm ", T.PERSON_FIRSTNAME, ", let's be friends!");
    }
    public static void OnTalk2(ContactedPlayer contact) {
        contact.TextHelper.ShowText("We should talk for a bit and get to know each other. And by talk I mean stand around and not really do anything.");
    }
    public static void OnTalk3(ContactedPlayer contact) {
        contact.TextHelper.ShowText("Yeahhhh that hit the spot! You're a great conversationalist, ", T.PLAYER_FIRSTNAME, "! I'm gonna tell everyone about you! Come find me again later~");
        contact.Connection.RaiseConnection(2);
    }

    public void RaiseConnection(int newConnection) {
        if (newConnection > Connection) {
            Connection = newConnection;
            switch (Connection) {
                case 1:
                    break;
                case 2:
                    controller.DontMove = false;
                    break;
                case 3:
                    break;
            }
        }
    }

    void Update() {
        color.FadeOutline = Connection < 1;
        color.FadeColor = Connection < 2;
        foreach (var mesh in meshes) {
            mesh.OutlineShake = Connection >= 3 ? 0.8f : Connection >= 2 ? 0.15f : 0;
        }
    }
}
