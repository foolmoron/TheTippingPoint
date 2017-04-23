using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContent : MonoBehaviour {

    public static readonly string[] STARS = { "-", "*", "* *", "* * *" };

    public GameObject Template;
    public float ItemHeight = 30;
    ListItem[] items;

    public float UpdateInterval = 1;
    float updateTime;

    RectTransform rt;

    IEnumerator Start() {
        rt = GetComponent<RectTransform>();

        items = new ListItem[People.Inst.Persons.Length];
        for (int i = 0; i < items.Length; i++) {
            var item = Instantiate(Template, transform).GetComponent<RectTransform>();
            item.localPosition = new Vector3(0, -i * ItemHeight, 0);
            item.localScale = Vector3.one;
            item.sizeDelta = new Vector2(0, 30);
            items[i] = item.GetComponent<ListItem>();
        }

        rt.sizeDelta = rt.sizeDelta.withY(items.Length * ItemHeight);

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        var infos = People.Inst.PersonInfos;
        var colors = People.Inst.PersonColors;
        for (int i = 0; i < infos.Length; i++) {
            items[i].First.text = infos[i].FirstName;
            items[i].Last.text = infos[i].LastName;
            items[i].Size.text = infos[i].Size.ToString("0.00");
        }
        for (int i = 0; i < colors.Length; i++) {
            items[i].Colors.text = string.Format("{0:0.0}/{1:0.0}/{2:0.0}/{3:0.0}", colors[i].HeadHue, colors[i].ShirtHue, colors[i].PantsHue, colors[i].OutlineColor.r);
        }
    }
	
	void Update() {
        updateTime -= Time.deltaTime;
        if (updateTime < 0) {
            var people = People.Inst.Persons;
            var connections = People.Inst.PersonConnections;

            for (int i = 0; i < people.Length; i++) {
                items[i].Position.text = string.Format("[{0:00}/{1:00}]", people[i].transform.position.x, people[i].transform.position.y);
            }
            for (int i = 0; i < connections.Length; i++) {
                items[i].Level.text = STARS[connections[i].Connection];
            }

            updateTime = UpdateInterval;
        }
    }
}
