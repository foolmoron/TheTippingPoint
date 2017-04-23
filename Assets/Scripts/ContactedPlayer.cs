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
        if (!collision.GetComponent<Player>() || CurrentlyContacting != null) {
            return;
        }
        InContact = true;
        ContactId++;
        CurrentlyContacting = this;
        OnContactEnter(this);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) {
            return;
        }
        InContact = false;
        CurrentlyContacting = null;
        OnContactExit(this);
    }
}
