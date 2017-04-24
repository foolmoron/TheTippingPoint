using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Manager<Player> {

    public PersonInfo Info;
    public PersonColor Color;

    public GameObject StartObj;

    void Awake()
    {
        Info = GetComponent<PersonInfo>();
        Color = GetComponent<PersonColor>();
    }

    public void SetFirstName(string s)
    {
        Info.FirstName = string.IsNullOrEmpty(s) ? NameGenerator.Inst.GetRandomFirstName() : s.Length > 20 ? s.Substring(0, 20) : s;
    }
    public void SetLastName(string s)
    {
        Info.LastName = string.IsNullOrEmpty(s) ? NameGenerator.Inst.GetRandomLastName() : s.Length > 20 ? s.Substring(0, 20) : s;
    }
    public void SetHead(float h)
    {
        Color.HeadHue = h;
    }
    public void SetShirt(float h)
    {
        Color.ShirtHue = h;
    }
    public void SetPants(float h)
    {
        Color.PantsHue = h;
    }
    public void SetSize(float h)
    {
        Info.Size = Mathf.Lerp(0.9f, 1.1f, h);
    }

    public void Done() {
        DayManager.Inst.Started = true;
        Destroy(StartObj);
    }
}
