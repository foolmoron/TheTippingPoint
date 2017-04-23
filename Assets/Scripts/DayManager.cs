using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayManager : Manager<DayManager> {

    public event Action<int> OnNewDay = delegate { };

    public const int MONTH_COUNT = 10;
    static readonly string[] MonthPrefixes = new string[MONTH_COUNT] { "Jan", "Sam", "Fe", "Ap", "Septem", "Ju", "Bro", "Octo", "Wil", "Lol" };
    static readonly string[] MonthSuffixes = new string[MONTH_COUNT] { "uary", "uary", "bruary", "ril", "ber", "ly", "bruary", "ber", "ch", "fer" };
    public static string[] Months;

    public const int DAY_COUNT = 6;
    static readonly string[] DayPrefixes = { "Nom", "Joe", "Egg", "Doot", "Yolo", "Tyler", "Poop", "Cray", "Wut", "Ayy", "Meme", "You", "Why", "Marx", "Two", "Some", "Any", "Yad", "Foo", "Zon" };
    public static string[] Days;

    public const int DAYS_PER_MONTH = 10;

    public int Day = 1;
    public string DayOfWeek;
    public string Month;
    public int DayOfMonth;

    public float DaySeconds = 30;
    public float TimeToNextDay;

    public void Awake() {
        var prefixes = MonthPrefixes.Shuffle();
        var suffixes = MonthSuffixes.Shuffle();
        Months = Enumerable.Range(0, MONTH_COUNT).Select(i => prefixes[i] + suffixes[i]).ToArray();

        var days = DayPrefixes.Shuffle();
        Days = new string[DAY_COUNT];
        for (int i = 0; i < Days.Length; i++) {
            Days[i] = days[i] + "day";
        }

        TimeToNextDay = DaySeconds;
    }

    public string ToFullString() {
        return string.Format("{0}, {1} {2}", DayOfWeek, Month, DayOfMonth);
    }

    void FixedUpdate() {
        var dayPassed = false;

        // cycle
        TimeToNextDay -= Time.deltaTime;
        if (TimeToNextDay <= 0) {
            Day++;
            TimeToNextDay = DaySeconds;
            dayPassed = true;
        }

        // day vars
        var dayIndex = Mathf.Max(Day - 1, 0);
        DayOfWeek = Days[dayIndex % DAY_COUNT];
        Month = Months[(dayIndex / MONTH_COUNT) % MONTH_COUNT];
        DayOfMonth = (dayIndex % MONTH_COUNT) + 1;

        // event
        if (dayPassed) {
            OnNewDay(Day);
        }
    }
}
