using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Nascimento.Model;

namespace Nascimento.Dev
{
    public class StandardListWindowFilter : IListWindowFilter
    {
        private int lastLvl = int.MinValue;

        public bool CanShow(LevelSO levelSO)
        {
            return true;
        }

        public void CategoryLabel(LevelSO levelSO)
        {
            var aValue = 0;
            if (levelSO.Item.Components == null)
                aValue = -1;
            else
                aValue = levelSO.Item.Components.Length;

            if (aValue != lastLvl)
            {
                GUILayout.Space(15);
                GUILayout.Label($"Components {aValue}", EditorStyles.boldLabel);
                GUILayout.Space(15);
                lastLvl = aValue;
            }
        }

        public void Sort(List<LevelSO> levelSOs)
        {
            levelSOs.Sort();
        }
    }
}