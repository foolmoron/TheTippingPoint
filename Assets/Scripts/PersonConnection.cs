using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PersonConnection : MonoBehaviour {

    public static readonly string[][] GREETINGS = {
        T.s("Hi ", T.PLAYER_FIRSTNAME, "! I'm ", T.PERSON_FIRSTNAME, ", let's be friends!"),
        T.s("Hey ", T.PLAYER_FIRSTNAME, "! My name is ", T.PERSON_FIRSTNAME, ", let's be friends!"),
        T.s("Hello ", T.PLAYER_FIRSTNAME, "! I've been looking for something to talk to! I'm ", T.PERSON_FIRSTNAME, "."),
        T.s("Yo what's your name? I'm ", T.PERSON_FIRSTNAME, "!"),
        T.s(T.PLAYER_FIRSTNAME, " huh? Can I ask you something?..."),
        T.s("Oh you're ", T.PLAYER_FIRSTNAME, "? Let's talk fast, I have to submit my Ludum Dare game in 30 minutes!"),
        T.s("WELCOME PLAYER FIRSTNAME:", T.PLAYER_FIRSTNAME, " LASTNAME:", T.PLAYER_LASTNAME, " TO THIS INTERACTIVE VIDEO EXPERIENCE"),
        T.s("Snape kills Dumbledoor! That's a Wizards of the Lost Coast spoiler ;D"),
    };

    public static readonly string[][] INTERJECTIONS = {
        T.s("Yeahhhh that hits the spot! "),
        T.s("Brrr it's cold! "),
        T.s("Am I just saying random things? "),
        T.s("Anyways... "),
        T.s("I mean - "),
        T.s("What was I saying again? Oh - "),
        T.s("How grossly incandescent. "),
        T.s("[insert Rick & Morty reference] "),
    };

    public static readonly string[][] PHRASES = {
        T.s("We should talk for a bit and get to know each other. And by talk I mean stand around..."),
        T.s("Have you ever truly challenged yourself in life?"),
        T.s("Did you know it's really tedious writing random dialogue?"),
        T.s("I shouldn't be writing this crap so close to the deadline..."),
        T.s("You should play this really cool visual novel game called Kawaii Aishiteru Wormhole Adventure!"),
        T.s("If you like puzzle games like The Witness you should check out The Cloister!"),
        T.s("Twitter doesn't exist in this world, but if it did, I would totally follow @foolmoron!"),
        T.s("Sorry if this conversation is boring for you..."),
        T.s("Sorry if you don't enjoy this game, it's hard to please everyone..."),
        T.s("I'm sorry if you had this conversation already. It's new to me."),
        T.s("I don't care if we've had this conversation already, I really value your company."),
    };

    public static readonly string[][] UPGRADES = {
        T.s("You're a great conversationalist, ", T.PLAYER_FIRSTNAME, "! I'm gonna tell everyone about you!"),
        T.s("You're so cool, ", T.PLAYER_FIRSTNAME, "! I'm glad I talked to you <3"),
        T.s("ACHIEVEMENT UNLOCKED!... just kidding! Or am I...? I am."),
        T.s("Thanks so much for your time ", T.PLAYER_FIRSTNAME, "! I know it is valuable."),
        T.s("You're amazing and so is your Ludum Dare game (if you have one), ", T.PLAYER_FIRSTNAME, "!"),
        T.s("I loved this conversation, ", T.PLAYER_FIRSTNAME, ". Let's do it again soon."),
    };

    public static readonly string[][] CLOSERS = {
        T.s("HEY has anyone heard of ", T.PLAYER_FIRSTNAME, "????"),
        T.s("I'm Captain ", T.PERSON_FIRSTNAME, "! Don't believe ", T.PLAYER_LASTNAME, "'s lies!... just kidding!"),
        T.s("Praise the sun!"),
        T.s("I need scissors! 61!"),
        T.s("Wheeeeee~"),
        T.s("I'm hungry!"),
        T.s("I really need to sleep more"),
        T.s("DUN DUN DUN DUN DUNNN DUNNN DUN-DUN DU-DUNNNNNNN"),
        T.s("I'm not bored anymore!"),
        T.s("What a small world LOL get it??"),
    };

    [Range(0, 3)]
    public int Connection;
    public int Points;

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
    SpreadConnection spread;

    void Awake() {
        color = GetComponent<PersonColor>();
        meshes = GetComponentsInChildren<Mesh2D>();

        controller = GetComponent<PersonController>();

        spread = GetComponentInChildren<SpreadConnection>();

        contact = GetComponentInChildren<ContactedPlayer>();
        contact.OnSeen += OnSeen;
        contact.OnTalk1 += OnTalk1;
        contact.OnTalk2 += OnTalk2;
        contact.OnTalk3 += OnTalk3;
        contact.OnTalk4 += OnTalk4;
    }

    public static void OnSeen(ContactedPlayer contact) {
        contact.Connection.DayLastSeen = DayManager.Inst.Day;
    }

    public static void OnTalk1(ContactedPlayer contact) {
        contact.Connection.DayLastTalked = DayManager.Inst.Day;
        if (contact.Connection.Introducer != null && contact.Connection.Introducer != Player.Inst.Info) {
            contact.TextHelper.ShowText(T.s("Oh hey you're ", T.INTRO, "'s friend! What's up?"));
        } else {
            contact.TextHelper.ShowText(contact.AlreadyTalked ? T.s("Hey ", T.PLAYER_FIRSTNAME, "! We already talked today, but I guess we can talk some more!") : GREETINGS.Random());
        }
    }
    public static void OnTalk2(ContactedPlayer contact) {
        contact.TextHelper.ShowText(INTERJECTIONS.Random(), PHRASES.Random());
    }
    public static void OnTalk3(ContactedPlayer contact) {
        contact.TextHelper.ShowText(INTERJECTIONS.Random(), UPGRADES.Random());
    }
    public static void OnTalk4(ContactedPlayer contact) {
        contact.TextHelper.ShowText(CLOSERS.Random());
        if (!contact.AlreadyTalked) {
            contact.Connection.RaiseConnection(contact.Connection.Connection + 1);
        }
        contact.Connection.controller.DontMove = false;
    }

    public void RaiseConnection(int newConnection) {
        Connection = Mathf.Clamp(newConnection, Connection, 3);
    }

    void Update() {
        color.FadeOutline = Connection < 1;
        color.FadeColor = Connection < 2;
        foreach (var mesh in meshes) {
            mesh.OutlineShake = Connection >= 3 ? 0.8f : Connection >= 2 ? 0.15f : 0;
        }
    }
}
