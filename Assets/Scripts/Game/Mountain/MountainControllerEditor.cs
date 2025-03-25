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
    }
}
#endif
