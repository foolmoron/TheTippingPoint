using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ListContent2 : MonoBehaviour {

    public static readonly string[] STARS = { "     ", "  *  ", " * * ", "~*^*~" };
    static string[] NUMS;

    public Text[] Texts;
    public float TextHeight = 300;

    public float UpdateInterval = 1;
    float updateTime;

    int N;

    RectTransform rt;
    List<string>[] stringLists;
    StringBuilder sb = new StringBuilder();

    IEnumerator Start() {
        NUMS = new string[100];
        for (int i = 0; i < NUMS.Length; i++) {
            NUMS[i] = (i < 10 ? "0" : "") + i;
        }

        rt = GetComponent<RectTransform>();
        rt.sizeDelta = rt.sizeDelta.withY(Texts.Length * TextHeight);

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        N = People.Inst.Persons.Length / Texts.Length;
        stringLists = Enumerable.Range(0, Texts.Length).Select(i => new List<string>()).ToArray();

        for (int s = 0; s < stringLists.Length; s++) {
            var stringList = stringLists[s];
            for (int n = 0; n < N; n++) {
                var i = s * N + n;
                stringList.Add(PadTo(i.ToString(), 3) + " ");
                stringList.Add(PadTo(People.Inst.PersonInfos[i].FirstName, 15) + " ");
                stringList.Add(PadTo(People.Inst.PersonInfos[i].LastName, 15));
                stringList.Add(string.Format("{0:.00}/{1:.00}/{2:.00}/{3:.00}", People.Inst.PersonColors[i].HeadHue, People.Inst.PersonColors[i].ShirtHue, People.Inst.PersonColors[i].PantsHue, HSBColor.FromColor(People.Inst.PersonColors[i].OutlineColor).h));
                stringList.Add(string.Format("   {0:0.00}   ", People.Inst.PersonInfos[i].Size));
                stringList.Add(""); // level placeholder
                stringList.Add("   [");
                stringList.Add(""); // position x placeholder;
                stringList.Add(", ");
                stringList.Add(""); // position y placeholder;
                stringList.Add("]\n");
            }
        }
    }

    string PadTo(string s, int length) {
        if (s.Length > length) {
            s = s.Substring(0, length);
        } else if (s.Length < length) {
            s = s + new string(' ', length - s.Length);
        }
        return s;
    }

    public void CullTexts(Vector2 scrollPos) {
        var y = Mathf.FloorToInt((1 - (scrollPos.y * 0.9f + 0.1f)) * 10);
        for (int i = 0; i < Texts.Length; i++) {
            Texts[i].gameObject.SetActive(i == y || (i - 1) == y);
        }
    }
	
	void Update() {
        updateTime -= Time.deltaTime;
        if (updateTime < 0 && stringLists != null) {
            var people = People.Inst.Persons;
            var conns = People.Inst.PersonConnections;
            for (int s = 0; s < stringLists.Length; s++) {
                var stringList = stringLists[s];
                for (int n = 0; n < N; n++) {
                    var i = s * N + n;
                    stringList[n * 11 + 5] = STARS[conns[i].Connection];
                    stringList[n * 11 + 7] = NUMS[(int) people[i].transform.position.x];
                    stringList[n * 11 + 9] = NUMS[(int) people[i].transform.position.y];
                }
            }
            for (int t = 0; t < Texts.Length; t++) {
                sb.Length = 0;
                var stringList = stringLists[t];
                foreach (var s in stringList) {
                    sb.Append(s);
                }
                Texts[t].text = sb.ToString();
            }

            updateTime = UpdateInterval;
        }
    }

    void OnEnable() {
        updateTime = 0;
    }
}
