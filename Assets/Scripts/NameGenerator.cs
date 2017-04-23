using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : Manager<NameGenerator> {

    public TextAsset FirstNamePrefixes;
    public TextAsset FirstNameSuffixes;
    public TextAsset LastNamePrefixes;
    public TextAsset LastNameSuffixes;

    string[] firstNamePrefixes;
    string[] firstNameSuffixes;
    string[] lastNamePrefixes;
    string[] lastNameSuffixes;

    void Do()
    {
        firstNamePrefixes = FirstNamePrefixes.text.Split('\n').Map(s => s.Replace("\r", ""));
        firstNameSuffixes = FirstNameSuffixes.text.Split('\n').Map(s => s.Replace("\r", ""));
        lastNamePrefixes = LastNamePrefixes.text.Split('\n').Map(s => s.Replace("\r", ""));
        lastNameSuffixes = LastNameSuffixes.text.Split('\n').Map(s => s.Replace("\r", ""));
    }

    public string GetRandomFirstName() {
        if (firstNamePrefixes == null) Do();
        var i1 = Mathf.FloorToInt(firstNamePrefixes.Length * Random.value);
        var i2 = Random.value > 0.5f ? i1 : Mathf.FloorToInt(firstNameSuffixes.Length * Random.value);
        return firstNamePrefixes[i1] + firstNameSuffixes[i2];
    }
    public string GetRandomLastName() {
        if (firstNamePrefixes == null) Do();
        var i1 = Mathf.FloorToInt(lastNamePrefixes.Length * Random.value);
        var i2 = Random.value > 0.5f ? i1 : Mathf.FloorToInt(lastNameSuffixes.Length * Random.value);
        return lastNamePrefixes[i1] + lastNameSuffixes[i2];
    }
}
