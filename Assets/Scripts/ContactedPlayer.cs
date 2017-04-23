using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactedPlayer : MonoBehaviour {

    public event Action<ContactedPlayer> OnContactEnter = delegate { };
    public event Action<ContactedPlayer> OnContactExit = delegate { };
    public event Action<ContactedPlayer> OnTalk = delegate { };

    public static ContactedPlayer CurrentlyContacting;

    public bool InContact;
    public int ContactId;

    public float TalkTime = 2.5f;
    public float CurrentContactTime;

    public PersonController Controller;
    public PersonTextHelper TextHelper;
    public PersonConnection Connection;

    void FixedUpdate() {
        if (InContact) {
            var wasTalkTime = CurrentContactTime >= TalkTime;
            CurrentContactTime += Time.deltaTime;
            if (!wasTalkTime && CurrentContactTime >= TalkTime) {
                OnTalk(this);
            }
        } else {
            CurrentContactTime = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != Player.Inst.gameObject || CurrentlyContacting != null) {
            return;
        }
        InContact = true;
        ContactId++;
        CurrentlyContacting = this;
        Controller.DontMove = true;
        OnContactEnter(this);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != Player.Inst.gameObject) {
            return;
        }
        InContact = false;
        CurrentlyContacting = null;
        Controller.DontMove = false;
        OnContactExit(this);
    }
}
