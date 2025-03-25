using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Nascimento.Model;

namespace Nascimento.Dev
{
    public class MultipleComponentFilter : IListWindowFilter
    {
        private int lastLvl = int.MinValue;

        public MultipleComponentFilter()
        {
        }

        public bool CanShow(LevelSO levelSO)
        {
            return levelSO.Item.Components != null && levelSO.Item.Components.Length > 0;
        }

        public void CategoryLabel(LevelSO levelSO)
        {
            var value = GetValue(levelSO);
            if (value != lastLvl)
            {
                GUILayout.Space(15);
                GUILayout.Label($"Components {value}", EditorStyles.boldLabel);
                GUILayout.Space(15);
                lastLvl = value;
            }
        }

        public void Sort(List<LevelSO> levelSOs)
        {
            levelSOs.Sort((a, b) =>
            {
                return GetValue(b).CompareTo(GetValue(a));
            });
        }

        static int GetValue(LevelSO a)
        {
            var aValue = 0;
            if (a.Item.Components == null)
                aValue = -1;
            else
            {
                for (var i = 0; i < a.Item.Components.Length; i++)
                {
                    aValue += a.Item.Components[i].Amount;
                }
            }

            return aValue;
        }
    }
}