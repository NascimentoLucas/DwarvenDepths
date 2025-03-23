using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Nascimento.Model;

namespace Nascimento.Dev
{
    public class MultipleComponentFilter : IListWindowFilter
    {
        private uint lastLvl = uint.MinValue;

        public MultipleComponentFilter()
        {
        }

        public bool CanShow(LevelSO levelSO)
        {
            return levelSO.Item.Components != null && levelSO.Item.Components.Length > 0;
        }

        public void CategoryLabel(LevelSO levelSO)
        {
            if (levelSO.MinLvl != lastLvl)
            {
                GUILayout.Space(15);
                GUILayout.Label($"Level {levelSO.MinLvl}", EditorStyles.boldLabel);
                GUILayout.Space(15);
                lastLvl = levelSO.MinLvl;
            }
        }

        public void Sort(List<LevelSO> levelSOs)
        {
            levelSOs.Sort((a, b) => a.MinLvl.CompareTo(b.MinLvl));
        }
    }
}