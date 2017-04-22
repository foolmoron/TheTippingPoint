using UnityEngine;
using System.Collections;

public class ShowNameOnTrigger : MonoBehaviour {

    public float LingerTime = 2;
    PersonInfo info;
    PersonColor color;

    bool canShowName = true;

    void Awake() {
        info = transform.parent.GetComponent<PersonInfo>();
        color = transform.parent.GetComponent<PersonColor>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (canShowName) {
            StartCoroutine(ShowNameAndWaitForLinger());
        }
    }

    IEnumerator ShowNameAndWaitForLinger() {
        canShowName = false;
        yield return TextBoxManager.Inst.ShowTextBox(info.FullName, false, transform.position + new Vector3(0, 0.5f * Random.value), color.OutlineColor, LingerTime);
        canShowName = true;
    }
}
