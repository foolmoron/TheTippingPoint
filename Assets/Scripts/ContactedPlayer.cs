using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactedPlayer : MonoBehaviour {

    public event Action<ContactedPlayer> OnContactEnter = delegate { };
    public event Action<ContactedPlayer> OnContactExit = delegate { };
    public event Action<ContactedPlayer> OnTalk1 = delegate { };
    public event Action<ContactedPlayer> OnTalk2 = delegate { };
    public event Action<ContactedPlayer> OnTalk3 = delegate { };

    public static ContactedPlayer CurrentlyContacting;

    public bool InContact;
    public int ContactId;

    public float Talk1Time = 2.5f;
    public float Talk2Time = 8f;
    public float Talk3Time = 15f;
    public float CurrentContactTime;

    public PersonController Controller;
    public PersonTextHelper TextHelper;
    public PersonConnection Connection;

    void FixedUpdate() {
        if (InContact) {
            var wasTalkTime1 = CurrentContactTime >= Talk1Time;
            var wasTalkTime2 = CurrentContactTime >= Talk2Time;
            var wasTalkTime3 = CurrentContactTime >= Talk3Time;
            CurrentContactTime += Time.deltaTime;
            if (!wasTalkTime1 && CurrentContactTime >= Talk1Time) {
                OnTalk1(this);
            }
            if (!wasTalkTime2 && CurrentContactTime >= Talk2Time) {
                OnTalk2(this);
            }
            if (!wasTalkTime3 && CurrentContactTime >= Talk3Time) {
                OnTalk3(this);
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
        if (collision.gameObject != Player.Inst.gameObject || CurrentlyContacting != this) {
            return;
        }
        InContact = false;
        CurrentlyContacting = null;
        Controller.DontMove = false;
        OnContactExit(this);
    }
}
