using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nascimento.Model
{
    [Serializable]
    public class ItemData
    {
        [field: Header("Setup")]
        [field: SerializeField]
        public ItemSO Item { get; private set; }

        [field: SerializeField]
        public int Amount { get; private set; }
    }


    [CreateAssetMenu(fileName = nameof(ItemSO), menuName = "Dev/SO/" + nameof(ItemSO))]
    public class ItemSO : ScriptableObject
    {
        [field: Header("Setup.Details")]
        [field: SerializeField]
        public string Name { get; private set; }

        [SerializeField]
        private string _descriptionText;

        [field: SerializeField]
        public Sprite Icon { get; private set; }


        [field: Header("Setup.Craft")]

        [field: SerializeField]
        public uint Steps { get; private set; } = 1;
        [field: SerializeField]
        public ItemData[] Components { get; private set; }


        public string Description
        {
            get
            {
                string s;
                if (Components.Length > 0)
                {
                    var args = new List<object> { Name };
                    foreach (var component in Components)
                    {
                        args.Add(component.Item.name);
                    }
                    s = string.Format(_descriptionText, args.ToArray());
                }
                else
                {
                    s = string.Format(_descriptionText, Name);
                }

                return s;
            }
        }

    }
}