﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : Manager<NameGenerator> {

    public TextAsset FirstNamePrefixes;
    public TextAsset FirstNameSuffixes;
    public TextAsset LastNamePrefixes;
    public TextAsset LastNameSuffixes;

    public string[] firstNamePrefixes;
    public string[] firstNameSuffixes;
    public string[] lastNamePrefixes;
    public string[] lastNameSuffixes;

    public void Awake() {
        firstNamePrefixes = FirstNamePrefixes.text.Split('\n');
        firstNameSuffixes = FirstNameSuffixes.text.Split('\n');
        lastNamePrefixes = LastNamePrefixes.text.Split('\n');
        lastNameSuffixes = LastNameSuffixes.text.Split('\n');
    }

    public string GetRandomFirstName() {
        var i1 = Mathf.FloorToInt(firstNamePrefixes.Length * Random.value);
        var i2 = Random.value > 0.5f ? i1 : Mathf.FloorToInt(firstNameSuffixes.Length * Random.value);
        return firstNamePrefixes[i1] + firstNameSuffixes[i2];
    }
    public string GetRandomLastName() {
        var i1 = Mathf.FloorToInt(lastNamePrefixes.Length * Random.value);
        var i2 = Random.value > 0.5f ? i1 : Mathf.FloorToInt(lastNameSuffixes.Length * Random.value);
        return lastNamePrefixes[i1] + lastNameSuffixes[i2];
    }
}