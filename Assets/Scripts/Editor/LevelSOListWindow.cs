using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Nascimento.Model;
using Codice.CM.SEIDInfo;

namespace Nascimento.Dev
{
    public class LevelSOListWindow : GuiStyles
    {
        protected GUIStyle _componentPanel;
        private Vector2 scrollPosition;
        private List<LevelSO> levelSOs = new List<LevelSO>();

        [MenuItem("Dev/" + nameof(LevelSOListWindow))]
        public static void ShowWindow()
        {
            var w = GetWindow<LevelSOListWindow>(nameof(LevelSOListWindow));
            w.SetupColor();
        }

        private void OnEnable()
        {
            RefreshLevelSOs();
        }

        private void RefreshLevelSOs()
        {
            SetupColor();

            levelSOs.Clear();
            string[] guids = AssetDatabase.FindAssets("t:LevelSO");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                LevelSO levelSO = AssetDatabase.LoadAssetAtPath<LevelSO>(path);
                Debug.Assert(levelSO != null, $"LevelSO not found at path: {path}");
                if (levelSO != null)
                {
                    levelSOs.Add(levelSO);
                }
            }
        }

        private void SetupColor()
        {
            _verticalPanel = CreateColorPanel(new Color(0.25f, 0.25f, 0.25f));
            _horizontalPanel = CreateColorPanel(new Color(0.25f, 0.25f, 0));
            _componentPanel = CreateColorPanel(new Color(0.25f, 0, 0.25f));
        }

        private void OnGUI()
        {
            GUILayout.Label("Level Scriptable Objects", EditorStyles.boldLabel);

            UseScroll(() =>
            {
                try
                {
                    if (GUILayout.Button("Refresh List"))
                    {
                        RefreshLevelSOs();
                    }


                    for (int i = 0; i < levelSOs.Count; i++)
                    {
                        UseVertical(() =>
                        {
                            GUILayout.Label(levelSOs[i].name, EditorStyles.boldLabel);
                            EditorGUILayout.ObjectField(levelSOs[i], typeof(LevelSO), false);

                            UseHorizontal(() =>
                            {
                                EditorGUILayout.LabelField($"Min: {levelSOs[i].minLvl}", GUILayout.Width(60));
                                EditorGUILayout.LabelField($"Max: {levelSOs[i].maxLvl}", GUILayout.Width(60));
                            }, _verticalPanel);

                            if (levelSOs[i].Item != null)
                            {
                                ShowItemSO(levelSOs[i].Item);
                            }
                        });
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    RefreshLevelSOs();
                }
            });
        }

        private void ShowItemSO(ItemSO item)
        {
            UseVertical(() =>
            {
                EditorGUILayout.ObjectField(item, typeof(ItemSO), false);
                if (item.Components != null &&
                item.Components.Length > 0)
                {
                    EditorGUILayout.LabelField("Components:");
                    for (int j = 0; j < item.Components.Length; j++)
                    {
                        UseHorizontal(() =>
                        {
                            EditorGUILayout.ObjectField(item.Components[j].Item, typeof(ItemSO), false);
                            EditorGUILayout.LabelField($"Amount: {item.Components[j].Amount}");
                            //ShowItemSO(item.Components[j].Item);
                        });
                    }
                }
            }, _componentPanel);
        }
    }
}