using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Nascimento.Model;

namespace Nascimento.Dev
{
    public class StandardListWindowFilter : IListWindowFilter
    {
        private uint lastLvl = uint.MinValue;

        public bool CanShow(LevelSO levelSO)
        {
            return true;
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