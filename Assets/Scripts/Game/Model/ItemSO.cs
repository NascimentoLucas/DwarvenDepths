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
        [field: Header("Setup")]
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public string Description { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }
        [field: SerializeField]
        public ItemData[] Components { get; private set; }

    }
}