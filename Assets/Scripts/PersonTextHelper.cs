using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public static class T {
    public const string PLAYER_FIRSTNAME = "{{playerfirst}}";
    public const string PLAYER_LASTNAME = "{{playerlast}}";
    public const string PLAYER_FULLNAME = "{{playerfull}}";
    public const string PERSON_FIRSTNAME = "{{personfirst}}";
    public const string PERSON_LASTNAME = "{{personlast}}";
    public const string PERSON_FULLNAME = "{{personfull}}";
}
public class PersonTextHelper : MonoBehaviour {

    public const int MAX_LINE_LENGTH = 26;

    public float LingerTime = 1.25f;

    public PersonInfo info;
    PersonColor color;
    StringBuilder sb = new StringBuilder();

    void Awake() {
        info = GetComponent<PersonInfo>();
        color = GetComponent<PersonColor>();
    }
    
    public void ShowText(params string[] texts) {
        // replacement
        sb.Length = 0;
        foreach (var text in texts) {
            switch (text) {
                case T.PLAYER_FIRSTNAME:
                    sb.Append(Player.Inst.Info.FirstName);
                    break;
                case T.PLAYER_LASTNAME:
                    sb.Append(Player.Inst.Info.LastName);
                    break;
                case T.PLAYER_FULLNAME:
                    sb.Append(Player.Inst.Info.FullName);
                    break;
                case T.PERSON_FIRSTNAME:
                    sb.Append(info.FirstName);
                    break;
                case T.PERSON_LASTNAME:
                    sb.Append(info.LastName);
                    break;
                case T.PERSON_FULLNAME:
                    sb.Append(info.FullName);
                    break;
                default:
                    sb.Append(text);
                    break;
            }
        }
        var fullText = sb.ToString();
        // line splitting
        sb.Length = 0;
        var words = fullText.Split(' ');
        var lineLength = 0;
        foreach (var word in words) {
            if (lineLength > 0) {
                sb.Append(" ");
                lineLength++;
            }
            if (lineLength + word.Length >= MAX_LINE_LENGTH) {
                sb.Append(word);
                sb.Append(" ");
                sb.AppendLine();
                lineLength = 0;
            } else {
                sb.Append(word);
                lineLength += word.Length;
            }
        }
        var finalText = sb.ToString().TrimEnd();
        // show text box
        StartCoroutine(TextBoxManager.Inst.ShowTextBox(finalText, true, transform.position + new Vector3(0, 0.5f, 0), transform, color.OutlineColor, LingerTime));
    }
}
