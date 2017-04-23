using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxManager : Manager<TextBoxManager> {

    public GameObject TextBoxPrefab;
    ObjectPool textBoxPool;
    public float TextBoxZ;

    public float LetterInterval = 0.05f;
    public float WordInterval = 0.2f;

    public void Awake() {
        textBoxPool = TextBoxPrefab.GetObjectPool(10);
    }

    public IEnumerator ShowTextBox(string text, bool byWord, Vector2 pos, Transform parent, Color outlineColor, float lingerTime) {
        var textBox = textBoxPool.Obtain().GetComponent<TextBox>();
        // init
        {
            textBox.transform.parent = parent;
            textBox.transform.position = pos.to3(TextBoxZ);
            textBox.OutlineColor = outlineColor;
            textBox.AutoScale = true;
        }
        // text anim
        var s = "";
        textBox.SetText(s);
        var items = byWord ? text.Split(' ') : text.ToCharArray().Map(c => c.ToString());
        foreach (var item in items) {
            yield return new WaitForSeconds(byWord ? WordInterval : LetterInterval);
            s += item;
            textBox.SetText(s);
        }
        // linger
        yield return new WaitForSeconds(lingerTime);
        textBox.gameObject.Release();
    }
}
