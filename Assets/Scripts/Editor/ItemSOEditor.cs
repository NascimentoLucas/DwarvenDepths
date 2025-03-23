using UnityEngine;
using UnityEditor;
using Nascimento.Model;

[CustomEditor(typeof(ItemSO))]
public class ItemSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var item = target as ItemSO;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Description Preview:\n" + item.Description, GUILayout.Height(100));
    }
}