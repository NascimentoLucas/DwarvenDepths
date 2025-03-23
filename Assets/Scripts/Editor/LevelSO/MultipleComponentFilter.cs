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
            if (levelSO.minLvl != lastLvl)
            {
                GUILayout.Space(15);
                GUILayout.Label($"Level {levelSO.minLvl}", EditorStyles.boldLabel);
                GUILayout.Space(15);
                lastLvl = levelSO.minLvl;
            }
        }

        public void Sort(List<LevelSO> levelSOs)
        {
            levelSOs.Sort((a, b) => a.minLvl.CompareTo(b.minLvl));
        }
    }
}