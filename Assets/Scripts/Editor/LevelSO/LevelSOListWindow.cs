using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Nascimento.Model;
using Codice.CM.SEIDInfo;
using Unity.VisualScripting;

namespace Nascimento.Dev
{

    public interface IListWindowFilter
    {
        bool CanShow(LevelSO levelSO);
        void CategoryLabel(LevelSO levelSO);
        void Sort(List<LevelSO> levelSOs);
    }


    public class LevelSOListWindow : GuiStyles
    {
        protected GUIStyle _componentPanel;
        private IListWindowFilter _filter;
        private List<LevelSO> levelSOs = new List<LevelSO>();

        [MenuItem("Dev/" + nameof(LevelSOListWindow))]
        public static void ShowWindow()
        {
            var w = GetWindow<LevelSOListWindow>(nameof(LevelSOListWindow));
            w.SetupColor();
        }

        void OnFocus()
        {
            RefreshLevelSOs();
        }

        private void OnEnable()
        {
            RefreshLevelSOs();
        }

        private void RefreshLevelSOs()
        {
            if (_filter == null)
                _filter = new StandardListWindowFilter();

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

            _filter.Sort(levelSOs);
        }

        private void SetupColor()
        {
            _verticalPanel = CreateColorPanel(new Color(0.25f, 0.25f, 0.25f));
            _horizontalPanel = CreateColorPanel(new Color(0.25f, 0.25f, 0));
            _componentPanel = CreateColorPanel(new Color(0.25f, 0, 0.25f));
        }

        private void OnGUI()
        {
            UseScroll(() =>
            {
                try
                {

                    GUILayout.Label("Level Scriptable Objects", EditorStyles.boldLabel);
                    if (GUILayout.Button("Refresh List"))
                    {
                        RefreshLevelSOs();
                    }

                    UseHorizontal(() =>
                    {
                        GUILayout.Label("Filtros", EditorStyles.boldLabel);
                        if (GUILayout.Button("Remover filtro"))
                        {
                            _filter = new StandardListWindowFilter();
                            RefreshLevelSOs();
                        }
                        if (GUILayout.Button("Filtrar multiplos componentes"))
                        {
                            _filter = new MultipleComponentFilter();
                            RefreshLevelSOs();
                        }
                    });

                    for (int i = 0; i < levelSOs.Count; i++)
                    {
                        if (!_filter.CanShow(levelSOs[i])) continue;

                        UseVertical(() =>
                        {
                            _filter.CategoryLabel(levelSOs[i]);

                            GUILayout.Label(levelSOs[i].name, EditorStyles.boldLabel);
                            EditorGUILayout.ObjectField(levelSOs[i], typeof(LevelSO), false);

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


        private void ShowItem(ItemSO item)
        {
            UseHorizontal(() =>
            {
                EditorGUILayout.ObjectField(item, typeof(ItemSO), false);
                if (item.Icon != null)
                {
                    GUILayout.Label(new GUIContent(item.Icon.texture), GUILayout.Width(32), GUILayout.Height(32));
                }
            }, _componentPanel);
        }

        private void ShowItemSO(ItemSO item)
        {
            UseVertical(() =>
            {
                GUILayout.Label($"{item.name}:\n{item.Description}");
                ShowItem(item);
                if (item.Components != null &&
                item.Components.Length > 0)
                {
                    EditorGUILayout.LabelField("Components:");
                    for (int j = 0; j < item.Components.Length; j++)
                    {
                        UseHorizontal(() =>
                        {
                            ShowItem(item.Components[j].Item);
                            EditorGUILayout.LabelField($"Amount: {item.Components[j].Amount}");
                        });
                    }
                }
            }, _componentPanel);
        }
    }
}