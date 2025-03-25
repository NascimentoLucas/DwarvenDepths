#if UNITY_EDITOR
using Nascimento.Model;
using NaughtyAttributes;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Nascimento.Game.Mountain
{
    public partial class MountainController
    {
        [Button]
        public void GetAllLevels()
        {
            List<LevelSO> levelSOs = new List<LevelSO>();
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
            _levelSO = levelSOs.ToArray();
            EditorUtility.SetDirty(this);
        }


        [Button]
        public void SortLevels()
        {
            if (_levelSO == null || _levelSO.Length < 1) return;

            List<LevelSO> levelSOs = new List<LevelSO>();
            levelSOs.AddRange(_levelSO);
            levelSOs.Sort((a, b) =>
            {
                if (a.Item.Components != null && b.Item.Components != null)
                {
                    return a.Item.Components.Length.CompareTo(b.Item.Components.Length);
                }

                return a.MinLvl.CompareTo(b.MinLvl);
            });
            _levelSO = levelSOs.ToArray();
            EditorUtility.SetDirty(this);
        }
    }
}
#endif
