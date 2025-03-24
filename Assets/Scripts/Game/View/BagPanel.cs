using Nascimento.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nascimento.View
{

    public class BagPanel : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private ItemView _prefab;
        [SerializeField]
        private LayoutGroup _content;
        [SerializeField]
        private RectTransform _root;

        private readonly Dictionary<ItemSO, ItemView> _items = new Dictionary<ItemSO, ItemView>();


        private void MoveToFront(ItemSO item)
        {
            if (_items.ContainsKey(item))
            {
                _items[item].transform.SetAsFirstSibling();
            }
        }

        public void Setup(ItemSO item, int amount)
        {
            if (!_items.ContainsKey(item))
            {
                var itemView = Instantiate(_prefab, _content.transform);
                itemView.Setup(item.Icon, $"{item.Name}: {amount.ToString("00")}");
                itemView.name = $"{item.Name}.View";
                _items[item] = itemView;
            }
            else
            {
                _items[item].UpdateText($"{item.Name}: {amount.ToString("00")}");
            }
        }

        internal void SetAsFirstItem(ItemSO item)
        {
            for (int i = 0; i < item.Components.Length; i++)
            {
                MoveToFront(item.Components[i].Item);
            }

            MoveToFront(item);
        }
    }
}
