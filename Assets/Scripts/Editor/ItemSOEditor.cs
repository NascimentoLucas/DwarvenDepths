using UnityEngine;
using UnityEditor;
using Nascimento.Model;

namespace Nascimento.Dev
{
    [CustomEditor(typeof(ItemSO))]
    public class ItemSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (target is ItemSO item)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Description Preview:\n" + item.Description, GUILayout.Height(100));
            }
        }
    }
}