using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class String : PropertyAttribute
{
    public string text;
    public float r, g, b;
    public String(string input)
    {
        text = input;
    }

}

[CustomPropertyDrawer(typeof(String))]
public class StringDraw : DecoratorDrawer
{
    String str
    {
        get { return ((String)attribute); }
    }
    public override float GetHeight()
    {
        return base.GetHeight() + 25;
    }
    public override void OnGUI(Rect position)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = new Color(0.4f, 0.8f, 0.66f);
        style.font = EditorStyles.boldFont;
        style.fontSize = 14;
        Color oldGuiColor = GUI.color;
        GUI.Label(new Rect(position.x, position.y + 20, 300, 25), str.text, style);
        GUI.color = oldGuiColor;
    }
}
#endif