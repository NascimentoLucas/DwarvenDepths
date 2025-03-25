using System;
using Nascimento.Game.Level.Controller;
using UnityEngine;

namespace Nascimento.Model
{

    [CreateAssetMenu(fileName = nameof(LevelSO), menuName = "Dev/SO/" + nameof(LevelSO))]
    public partial class LevelSO : ScriptableObject, IComparable
    {
        [field: Header("Setup")]
        [field: SerializeField]
        public ItemSO Item { get; private set; }
        [field: SerializeField]
        public LevelController Prefab { get; private set; }

        public int CompareTo(object obj)
        {
            if (obj is LevelSO b)
            {
                var aValue = 0;
                if (Item.Components == null)
                    aValue = -1;
                else
                    aValue = Item.Components.Length;

                var bValue = 0;
                if (b.Item.Components == null)
                    bValue = -1;
                else
                    bValue = b.Item.Components.Length;

                return aValue.CompareTo(bValue); 
            }

            throw new ArgumentException($"Object is not a {nameof(LevelSO)}");
        }
    }
}